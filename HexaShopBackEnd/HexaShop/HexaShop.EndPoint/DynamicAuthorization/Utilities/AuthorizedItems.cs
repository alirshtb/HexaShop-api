using System;

namespace HexaShop.ApiEndPoint.DynamicAuthorization.Utilities
{
    public class AuthorizedItems : IEquatable<AuthorizedItems>
    {
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public string AuthorizedClaimValue { get; set; }

        public AuthorizedItems(string controllerName, string actionName, string authorizedClaimValue)
        {
            ControllerName = controllerName;
            ActionName = actionName;
            AuthorizedClaimValue = authorizedClaimValue;
        }

        public bool Equals(AuthorizedItems other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            if (GetType() != other.GetType())
            {
                return false;
            }

            return other.ControllerName == ControllerName && other.ActionName == ActionName;
        }


        //public bool Equals(AuthorizedItems other)
        //{
        //    if (ReferenceEquals(null, other)) return false;

        //    if (ReferenceEquals(this, other)) return true;

        //    if (GetType() != other.GetType()) return false;

        //    return other.ActionName == ActionName
        //         && other.ControllerName == ControllerName;
        //}

        public override bool Equals(object obj)
        {
            return Equals(obj as AuthorizedItems);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ControllerName, ActionName);
        }
    }
}
