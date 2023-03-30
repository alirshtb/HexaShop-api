using HexaShop.Application.Constracts.PersistanceContracts;
using HexaShop.Common;
using HexaShop.Domain;
using HexaShop.EndPoint.Models.ViewModels.AdminController;
using HexaShop.Persistance;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HexaShop.EndPoint.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AdminController : ControllerBase
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly HexaShopDbContext _dbContext;
        private readonly RoleManager<AppIdentityRole> _roleManager;

        public AdminController(IUnitOfWork unitOfWork, RoleManager<AppIdentityRole> roleManager, HexaShopDbContext dbContext)
        {
            _unitOfWork = unitOfWork;
            _roleManager = roleManager;
            _dbContext = dbContext;
        }

        /// <summary>
        /// get roles.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetRoles()
        {
            var roles = _dbContext.Roles.Where(r => r.Name != RoleNames.Admin).AsNoTracking().ToList();

            var rolesInModel = roles.Select(role => new RoleViewModel()
            {
                Id = role.Id,
                Name = role.Name,
                DisplayName = role.DisplayName
            }).ToList();

            return Ok(rolesInModel);

        }

        /// <summary>
        /// create a role.
        /// </summary>
        /// <param name="roleViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult CreateRole([FromBody] RoleViewModel roleViewModel)
        {
            var role = new AppIdentityRole()
            {
                Id = Guid.NewGuid().ToString(),
                Name = roleViewModel.Name,
                DisplayName = roleViewModel.DisplayName,
                NormalizedName = roleViewModel.Name.ToUpper(),
                ConcurrencyStamp = Guid.NewGuid().ToString()
            };


            using var transaction = _unitOfWork.BeginTransaction();
            _dbContext.Roles.Add(role);
            _unitOfWork.SaveChanges();

            var claims = roleViewModel.Claims.SelectMany(c => c.Claims);
            if (claims.Any())
            {
                _dbContext.RoleClaims.AddRange(claims.Select(c => new IdentityRoleClaim<string>()
                {
                    RoleId = role.Id,
                    ClaimType = c.Type,
                    ClaimValue = c.Value
                }));
                _unitOfWork.SaveChanges();
            }

            transaction.Commit();

            return CreatedAtAction("GetRoles", "");
        }

        /// <summary>
        /// edit a role accessiblities.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="roleViewModel"></param>
        /// <returns></returns>
        [HttpPatch("{id}")]
        public async Task<IActionResult> EditRole(string id, [FromBody] RoleViewModel roleViewModel)
        {
            var role = _dbContext.Roles.Find(id);

            if (role == null)
            {
                return NotFound(new { id = roleViewModel.Id, message = "Role Not Found." });
            }

            using var transaction = await _unitOfWork.BeginTransactionAsync();

            role.Name = roleViewModel.Name;
            role.DisplayName = roleViewModel.DisplayName;

            var roleClaims = await _roleManager.GetClaimsAsync(role);
            //roleClaims.ToList().ForEach(async c => await _roleManager.RemoveClaimAsync(role, c));
            foreach (var claim in roleClaims)
            {
                await _roleManager.RemoveClaimAsync(role, claim);
            }
            await _unitOfWork.SaveChangesAsync();

            var claims = roleViewModel.Claims.SelectMany(c => c.Claims);
            if (claims.Any())
            {
                await _dbContext.RoleClaims.AddRangeAsync(claims.Select(c => new IdentityRoleClaim<string>()
                {
                    ClaimType = c.Type,
                    ClaimValue = c.Value,
                    RoleId = role.Id
                }));
            }

            await _unitOfWork.SaveChangesAsync();

            await transaction.CommitAsync();

            return Ok(role);
        }

        /// <summary>
        /// remove a role.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<AppIdentityRole>> Delete(string id)
        {
            var role = await _dbContext.Roles.FindAsync(id);
            if (role == null)
            {
                return NotFound(new { id = id, message = "Role Not Found." });
            }

            _dbContext.Roles.Remove(role);
            await _unitOfWork.SaveChangesAsync();

            return role;
        }

    }
}
