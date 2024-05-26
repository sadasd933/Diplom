using System.ComponentModel.DataAnnotations;

namespace QualificationTest
{
    public class User
    {
        [Key]
        public int UsersID { get; set; }
        public string UsersName  { get; set; }
        public string UsersLogin{ get; set; }
        public string UsersPassword{ get; set; }
        public string UsersRole{ get; set; }


        public User() { }

        public User(int usersID, string usersName, string usersLogin, string usersPassword, string usersRole)
        {
            UsersID = usersID;
            UsersName = usersName;
            UsersLogin = usersLogin;
            UsersPassword = usersPassword;
            UsersRole = usersRole;
        }
    }
}

