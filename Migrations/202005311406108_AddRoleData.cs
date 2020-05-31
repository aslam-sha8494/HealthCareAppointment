namespace HealthCareAppointment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRoleData : DbMigration
    {
        public override void Up()
        {
            Sql("Insert Into dbo.UserRoles(RoleName) values('Admin');" +
               "Insert Into dbo.UserRoles(RoleName) values('Doctor');" +
               "Insert Into dbo.UserRoles(RoleName) values('Patient');");
        }
        
        public override void Down()
        {
        }
    }
}
