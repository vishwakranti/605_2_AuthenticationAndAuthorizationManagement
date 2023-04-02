# 605_2
Bike have copied the previous BikeSpareParts Website for authentication and authorization
We have set the following password properties to make it easier for debugging - RequireDigit = false;, RequireLowercase = false;, RequireNonAlphanumeric = false;, RequireUppercase = true;, RequiredLength = 6;, RequiredUniqueChars = 1;, RequireConfirmedEmail = false;
We have Modified the Register Page to automatically set confirm email to true.
We have Customised the Access Denied page to add an image of your own choice, something unique.
Added the given list of Staff into projects ASPNetUsers table via the Identity Register system(These guys are in your staff table already).
Created the roles of Admin, Manager, Staff, using the RoleManager interface(As per the saved database name and email)
Created the Staff User Claims using the claims manager interface.
Created the different staff policies and applied them
