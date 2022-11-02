using System.Diagnostics;
using System.Numerics;
using Microsoft.EntityFrameworkCore;
using StudentOffice.Models;

namespace StudentOffice.Data
{
    
    public static class SOInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            StudentOfficeContext context = applicationBuilder.ApplicationServices.CreateScope()
                .ServiceProvider.GetRequiredService<StudentOfficeContext>();
            try
            {

                //To randomly generate data
                Random random = new Random();

                if (!context.EmployeeRoles.Any())
                {
                    context.EmployeeRoles.AddRange(
                     new EmployeeRole
                     {
                         RoleName = "Account Manager"
                     }, new EmployeeRole
                     {
                         RoleName = "Production Manager"
                     }, new EmployeeRole
                     {
                         RoleName = "Designer"
                     }, new EmployeeRole
                     {
                         RoleName = "Technician"
                     }, new EmployeeRole
                     {
                         RoleName = "Jr. Technician"
                     }, new EmployeeRole
                     {
                         RoleName = "Engineer"
                     }, new EmployeeRole
                     {
                         RoleName = "Jr. Engineer"
                     }, new EmployeeRole
                     {
                         RoleName = "Developer"
                     }, new EmployeeRole
                     {
                         RoleName = "Security Analyst"
                     }, new EmployeeRole
                     {
                         RoleName = "Tester"
                     }
                     );
                    context.SaveChanges();
                }

                if (!context.Teams.Any())
                {
                    context.Teams.AddRange(
                    new Team
                    {
                        FirstName = "1",
                        LastName = "Management"
                    },
                    new Team
                    {
                        FirstName = "2",
                        LastName = "Marketing"
                    },
                    new Team
                    {
                        FirstName = "3",
                        LastName = "Support"
                    }
                    ,
                    new Team
                    {
                        FirstName = "4",
                        LastName = "FrontEnd"
                    }
                    ,
                    new Team
                    {
                        FirstName = "5",
                        LastName = "BackEnd"
                    }
                    ,
                    new Team
                    {
                        FirstName = "6",
                        LastName = "Testing"
                    }
                    ,
                    new Team
                    {
                        FirstName = "7",
                        LastName = "UxDesing"
                    }
                    ,
                    new Team
                    {
                        FirstName = "8",
                        LastName = "DevOpps"
                    }
                    ,
                    new Team
                    {
                        FirstName = "9",
                        LastName = "Security"
                    }
                    ,
                    new Team
                    {
                        FirstName = "10",
                        LastName = "Incident Response"
                    }
                    ,
                    new Team
                    {
                        FirstName = "11",
                        LastName = "System Desing"
                    }
                    ,
                    new Team
                    {
                        FirstName = "12",
                        LastName = "Business Analyse"
                    }
                    );
                    context.SaveChanges();
                }
                if (!context.Employees.Any())
                {
                    context.Employees.AddRange(
                    new Employee
                    {
                        FirstName = "Fred",
                        MiddleName = "Reginald",
                        LastName = "Flintstone",
                        EmployeeNumber = "1231231234",
                        DOB = DateTime.Parse("1955-09-01"),
                        SuggestionNumber = 6,
                        Phone = "9055551212",
                        EmployeeRoleID = context.EmployeeRoles.FirstOrDefault(d => d.RoleName.Contains("Tester")).ID,
                        TeamID = context.Teams.FirstOrDefault(d => d.FirstName == "1" && d.LastName == "Management").ID
                        //TeamID = 1,
                    },
                    new Employee
                    {
                        FirstName = "Wilma",
                        MiddleName = "Jane",
                        LastName = "Flintstone",
                        EmployeeNumber = "1321321324",
                        DOB = DateTime.Parse("1964-04-23"),
                        SuggestionNumber = 2,
                        Phone = "9055551212",
                        //TeamID = 1,
                        TeamID = context.Teams.FirstOrDefault(d => d.FirstName == "2" && d.LastName == "Marketing").ID
                    },
                    new Employee
                    {
                        FirstName = "Barney",
                        LastName = "Rubble",
                        EmployeeNumber = "3213213214",
                        DOB = DateTime.Parse("1964-02-22"),
                        SuggestionNumber = 2,
                        Phone = "9055551213",
                        EmployeeRoleID = context.EmployeeRoles.FirstOrDefault(d => d.RoleName.Contains("Engineer")).ID,
                        //TeamID = 1,
                        TeamID = context.Teams.FirstOrDefault(d => d.FirstName == "2" && d.LastName == "Marketing").ID
                    },
                    new Employee
                    {
                        FirstName = "Jane",
                        MiddleName = "Samantha",
                        LastName = "Doe",
                        EmployeeNumber = "1231231235",
                        SuggestionNumber = 2,
                        Phone = "9055551234",
                        //TeamID = 1,
                        TeamID = context.Teams.FirstOrDefault(d => d.FirstName == "2" && d.LastName == "Marketing").ID
                    }); ;
                    context.SaveChanges();
                }
                //Add Employees after Teams
              
                if (!context.Conditions.Any())
                {
                    string[] conditions = new string[] { "Production", "Implementation", "Marketing", "Management", "Instalation", "Painting", "Carving", "Cutting", "Compress", "Lagring", "Transport", "Human Resource" };

                    foreach (string condition in conditions)
                    {
                        Condition c = new Condition
                        {
                            ConditionName = condition
                        };
                        context.Conditions.Add(c);
                    }
                    context.SaveChanges();
                }
                //EmployeeCondition
                if (!context.EmployeeConditions.Any())
                {
                    context.EmployeeConditions.AddRange(
                        new EmployeeCondition
                        {
                            ConditionID = context.Conditions.FirstOrDefault(c => c.ConditionName == "Painting").ID,
                            EmployeeID = context.Employees.FirstOrDefault(p => p.LastName == "Flintstone" && p.FirstName == "Fred").ID
                        },
                        new EmployeeCondition
                        {
                            ConditionID = context.Conditions.FirstOrDefault(c => c.ConditionName == "Carving").ID,
                            EmployeeID = context.Employees.FirstOrDefault(p => p.LastName == "Flintstone" && p.FirstName == "Fred").ID
                        },
                        new EmployeeCondition
                        {
                            ConditionID = context.Conditions.FirstOrDefault(c => c.ConditionName == "Lagring").ID,
                            EmployeeID = context.Employees.FirstOrDefault(p => p.LastName == "Flintstone" && p.FirstName == "Wilma").ID
                        });
                    context.SaveChanges();
                }

                //Add more teams
                //if (context.Teams.Count() < 13)
                //{
                //    string[] firstNames = new string[] { "12", "13", "14", "15", "16", "17", "18", "19" };
                //    string[] lastNames = new string[] { "FrontEnd", "BackEnd", "Jones", "Bloggs", "Brown", "Smith", "Daniel" };

                //    //Loop through names and add more
                //    foreach (string lastName in lastNames)
                //    {
                //        foreach (string firstname in firstNames)
                //        {
                //            //Construct some details
                //            Team a = new Team()
                //            {
                //                FirstName = firstname,
                //                LastName = lastName,

                //            };
                //            context.Teams.Add(a);
                //        }
                //    }
                //    context.SaveChanges();
                //}

                //So we can gererate random data, create collections of the primary keys
                int[] teamIDs = context.Teams.Select(a => a.ID).ToArray();
                int teamIDCount = teamIDs.Length;// Why does this help efficiency?
                int[] employeeRoleIDs = context.EmployeeRoles.Select(a => a.ID).ToArray();
                int employeeRolesIDCount = employeeRoleIDs.Length;

                //More employee.  Now it gets more interesting because we
                //have Foreign Keys to worry about
                //and more complicated property values to generate
                if (context.Employees.Count() < 6)
                {
                    string[] firstNames = new string[] { "Lyric", "Antoinette", "Kendal", "Vivian", "Ruth", "Jamison", "Emilia", "Natalee", "Yadiel", "Jakayla", "Lukas", "Moses", "Kyler", "Karla", "Chanel", "Tyler", "Camilla", "Quintin", "Braden", "Clarence" };
                    string[] lastNames = new string[] { "Watts", "Randall", "Arias", "Weber", "Stone", "Carlson", "Robles", "Frederick", "Parker", "Morris", "Soto", "Bruce", "Orozco", "Boyer", "Burns", "Cobb", "Blankenship", "Houston", "Estes", "Atkins", "Miranda", "Zuniga", "Ward", "Mayo", "Costa", "Reeves", "Anthony", "Cook", "Krueger", "Crane", "Watts", "Little", "Henderson", "Bishop" };
                    int firstNameCount = firstNames.Count();

                    // Birthdate for randomly produced Employees 
                    // We will subtract a random number of days from today
                    DateTime startDOB = DateTime.Today;// More efficiency?
                    int counter = 1; //Used to help add some Employees to Medical Trials

                    foreach (string lastName in lastNames)
                    {
                        //Choose a random HashSet of 4 (Unique) first names
                        HashSet<string> selectedFirstNames = new HashSet<string>();
                        while (selectedFirstNames.Count() < 4)
                        {
                            selectedFirstNames.Add(firstNames[random.Next(firstNameCount)]);
                        }

                        foreach (string firstName in selectedFirstNames)
                        {
                            //Construct some Employee details
                            Employee employee = new Employee()
                            {
                                FirstName = firstName,
                                LastName = lastName,
                                MiddleName = lastName[1].ToString().ToUpper(),
                                EmployeeNumber = random.Next(2, 9).ToString() + random.Next(213214131, 989898989).ToString(),

                                Phone = random.Next(2, 10).ToString() + random.Next(213214131, 989898989).ToString(),
                                SuggestionNumber = (int)random.Next(2, 200),
                                DOB = startDOB.AddDays(-random.Next(60, 34675)),
                                TeamID = teamIDs[random.Next(teamIDCount)]
                            };
                            if (counter % 3 == 0)//Every third Employee gets assigned to a Employee Roles
                            {
                                employee.EmployeeRoleID = employeeRoleIDs[random.Next(employeeRolesIDCount)];
                            }
                            counter++;
                            context.Employees.Add(employee);
                            try
                            {
                                //Could be a duplicate employeenumber
                                context.SaveChanges();
                            }
                            catch (Exception)
                            {
                                //so skip it and go on to the next
                            }
                        }
                    }
                    //Since we didn't guarantee that every Team had
                    //at least one Employee assigned, let's remove Teams
                    //without any Employees.  We could do this other ways, but it
                    //gives a chance to show how to execute 
                    //raw SQL through our DbContext.
                    string cmd = "DELETE FROM Teams WHERE NOT EXISTS(SELECT 1 FROM Employees WHERE Teams.Id = Employees.TeamID)";
                    context.Database.ExecuteSqlRaw(cmd);

                }



            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.GetBaseException().Message);
            }



        }
    }
}
