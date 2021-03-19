# Codescovery JsonServices
  Useful library to convert a json object to json paths. Also provides the ability to override the  value from a json path. 
  
## Pre requisites 

  Your project must be running under .Net Core 3.1 or higher. 
  


## How to use

### Dependency Injection 
```
   public void ConfigureServices(IServiceCollection services)
        {
           ...
            services.AddJsonServices();
           ...
        }
 ``` 
 
 ### Factory 
 
 ``` 
 var jsonService = JsonServiceFactory.Create();
 ``` 
 
 
 
 ## Result
  Take as example the following json: 
  ``` 
    {
       "person":{
          "name":"FirstName",
          "age":30,
          "childs":[
             {
                "name":"child1",
                "age":16
             },
             {
                "name":"child2",
                "age":18
             }
          ],
          "likedFruits":[
             "apple",
             "banana"
          ]
       }
    }
  ```
  
  The following code: 
  ```
    var service = JsonServiceFactory.Create();
    var jsonPath = service.ConvertToJsonPath(json);
    var paths = jsonPath.GetJsonPaths();
  ```
   Will return an IDictiorary<string,string>  like this:
  ```
  {
    "person":"{'name':'FirstName','age':30,'childs':[{'name':'child1','age':16},{'name':'child2','age':18}],'likedFruits':['apple','banana']}",
    "person.name":"FirstName",
    "person.age":"30",
    "person.childs":"[{'name':'child1','age':16},{'name':'child2','age':18}]",
    "person.childs[0].name":"child1",
    "person.childs[0].age":"16",
    "person.childs[1].name":"child2",
    "person.childs[1].age":"18",
    "person.likedfruits":"['apple','banana']",
    "person.likedfruits[0]":"apple",
    "person.likedfruits[1]":"banana"
  }
  ``` 
  
  
  
  
  ### Overriding Values
  
  This library has the ability to override json values based on paths. 
  Take this 2 jsons as examples.
  
  JSON1
  ``` 
    {
       "person":{
          "name":"FirstName",
          "age":30,
          "childs":[
             {
                "name":"child1",
                "age":16
             },
             {
                "name":"child2",
                "age":18
             }
          ],
          "likedFruits":[
             "apple",
             "banana"
          ]
       }
    }
  ```
  
JSON2
  ``` 
  {
     "person":{
        "name":"FirstName and LastName",
        "age":34,
        "likedFruits":[
           "pineapple",
           "avocado",
           "strawberry"
        ]
     }
  }
```
  
  If you use the following code: 
  ```
    var service = JsonServiceFactory.Create();
    var result = service.OverrideJsonValuesFromPath(json2, json1, writeIdented: true);
  ```
  
  It will return: 
  ``` 
    {
       "person":{
          "name":"FirstName and LastName",
          "age":34,
          "childs":[
             {
                "name":"child1",
                "age":16
             },
             {
                "name":"child2",
                "age":18
             }
          ],
          "likedFruits":[
             "pineapple",
             "avocado",
             "strawberry"
          ]
       }
    }
  ```
