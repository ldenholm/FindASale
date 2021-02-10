# Code  Challenge

## Requirement

You are required to build a **Production ready** application where you can automatically assign a Salesperson to a customer, based on a set of rules.

We would like to see your Backend and Frontend skills. Don't stress about storing data in database - instead, use the attached JSON file as your data source. Use design patterns and utilise coding best practices whereever applicable.

If you are an experienced Java developer who likes to work on .NET, feel free to use Java in order to build the backend. 

**We are using .NET Core and Angular in our team. And we love clean code.**

## Scenario

All salespeople are assigned to one or more groups from the below list.

- Group A – Speak Greek
- Group B – Sports car specialist
- Group C – Family car specialist
- Group D – Tradie vehicle specialist

**Salespeople Information** 

*you can find this data in [salesperson.json](salesperson.json)*

1. Cierra Vega. (assigned to Group A)
2. Alden Cantrell. (assigned to Group B & Group D)
3. Kierra Gentry. (assigned to Group A & Group C)
4. ...

**Salesperson Assigning Rules**

Rules on assigning a specific salesperson to a customer are below, in order of priority.

If a salesperson is assigned to a customer, that person cannot be assigned to another customer at the same time. If there are no salespeople available, the application should return a message saying, &quot;All salespeople are busy. Please wait.&quot;

| **Customer** | **Looking for** | **Rules** |
| --- | --- | --- |
| **Speaks Greek** | Sports Car | - Assign someone who speaks greek and good with Sports car. <br/> - If cannot be found, assign someone who is good in Sports car. <br/> - If cannot be found, assign anyone randomly |
| **Speaks Greek** | Family Car | - Assign someone who speaks greek and good with Family car. <br/> - If cannot be found, assign someone who is good in Family car. <br/> - If cannot be found, assign anyone randomly |
| **Regardless of the language** | Tradie Vehicle | - Assign someone who is good with Tradie vehicles <br/> - If cannot be found, assign randomly. |
| **Doesn't speak Greek** | Sports Car | - Assign someone who is good in Sports car. <br/> - If cannot be found, assign anyone randomly. |
| **Doesn't speak Greek** | Family Car | - Assign someone who is good in Family car. <br/> - If cannot be found, assign anyone randomly. |
| **Speaks Greek** | Not looking for anything specific | - Assign someone who speaks greek. <br/> - If cannot be found, assign anyone randomly. |
| **Doesn't speak Greek** | Not looking for anything specific | - Assign anyone randomly. |

<hr/>

## Example Test Case

First Customer speaks Greek and is looking for a Family car – Assigned to Kierra Gentry

Second Customer speaks Greek and is looking for a Sports car – Assigned to Thomas Crane

Third Customer doesn't speak Greek and is looking for a Sports car – Assigned to Alden Cantrell

….

# General Notes:
I have extended the json database to include an isAvailable property to track the availability of salespeople. There is also a button on the site that allows one to reset the availability of all salespeople (this came in handy when testing the application logic and resetting via find/replace was tedious. There is an enum of CarType which was used in my initial design, this enum is no longer used although I have kept it in case the project required extensions. I have also registered Swagger UI so that the endpoints can be clearly seen, to see the this page just navigate to /swagger and the ui will load. Writer service handles all file write operations, whereas Salesperson service is read-based. 

# Deployment Instructions:
## Front-End Deployment
- Create environment.prod.ts file to save settings for prod env variables.
- Build angular application with ng build --prod --base-href /IISAppNameHere/
  --base-href renames the base href in index.html to: /IISAppNameHere/.
- Create folder on the deployment server C:\IISAppNameHere, copy contents of the dist folder in there.
- Create new application in IIS:
  - Sites => Add Application...
  - Alias = name of app, Physical path = the directory above (C:\IISAppNameHere), can create dedicated app pool if necessary.
  - Set up bindings, there will be some configuring of the DNS settings, that will be handled on Azure portal.
  - Can create a web.config file to redirect all extraneous requests to the app homepage (router in angular handles part of this internally also).
  
   ### API Deployment
 
 - In Azure portal go to Create a Resource => Web App.
 - Create a new resource group for application.
 - Enter details for app name (blahblah.azurewebsites.net), this takes care of DNS mapping.
 - Specify runtime stack, for this I'm using .net core 3.1.
 - OS = Linux or Windows, linux is typically cheaper and often faster.
 - Region = Aus, or wherever is closest to user base.
 - Click on change size and select one of the dev/test options for affordability, all the production choices are too spenno.
 - Double check all choices on the review step, and create.
 
 - Update environment variables in VS and set to Production build.
 - Open the source code in VS, right click on solution and select Publish.
 - Choose the publish target just created.
 - Select the resource group.
 - If any changes to the target framework must be changed then you can edit them in the publish prompt, however will need to update the web app in Azure.
