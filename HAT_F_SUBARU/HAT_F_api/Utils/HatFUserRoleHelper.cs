using HAT_F_api.CustomModels;

namespace HAT_F_api.Utils
{
    public class HatFUserRoleHelper
    {
        public static List<HatFUserRole> ParseRoleString(string roleString)
        {
            string[] roleStrings = roleString.Split(',', StringSplitOptions.RemoveEmptyEntries);

            var roles = new List<HatFUserRole>();
            foreach(var item in roleStrings)
            {
                if (Enum.TryParse<HatFUserRole>(item, out HatFUserRole result))
                {
                    roles.Add(result);
                }
            }

            return roles;
        }

        public static bool ContainsRole(string roleString, HatFUserRole targetRole)
        {
            var roles = ParseRoleString(roleString);
            bool result = ContainsRole(roles, targetRole);
            return result;
        }

        public static bool ContainsRole(IEnumerable<HatFUserRole> roles, HatFUserRole targetRole)
        {
            bool result = roles.Contains(targetRole);
            return result;
        }
    }
}
