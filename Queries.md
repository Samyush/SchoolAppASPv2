Getting started
####### @Author samyush

# Question: public class ApiResponse<T> => what does T stand for (Generic Classes) 

# Answer:   Generic type is declared by specifying a type parameter in an angle brackets after a type name
    eg: TypeName<T> where "T" is a type parameter 
    
    class DataStore<T>
    {
        public T Data {get; set;}
    }

    and when we create onject for DataStore we can specify any type of data type we want
    making it flexible for DataStore class to store any type of data as "T" is flexible:

    DataStore<String> store = new DataStore<String>();
    and also:
    DataStore<Int32> store1 = new DataStore<Int32>();

#################################################################################################
    to know the use of IMapper in UserController of web app of src

    SchoolEventsController.cs line 56 why cant we use Class().Method(Data) to pass data here as in flutter => because using of Interface sercvices it makes this happen and //
    //                                                                                                        if we are not to use dependencies then we can use it straight as 
    //                                                                                                        flutter

    dapper, ado.net and entity framework

#################################################################################################

### Status code: =>>  
    You could use StatusCode(???) to return any HTTP status code.

    Also, you can use dedicated results:

    Success:

    return Ok() ← Http status code 200
    return Created() ← Http status code 201
    return NoContent(); ← Http status code 204


    Client Error:

    return BadRequest(); ← Http status code 400
    return Unauthorized(); ← Http status code 401
    return NotFound(); ← Http status code 404

#################################################################################################

### Web API JWT
    //web API authentication JWT link https://www.c-sharpcorner.com/article/authentication-and-authorization-in-asp-net-core-web-api-with-json-web-tokens/

##################################################################################################
HttpContext => SignInAsync || SignOutAsync (url link : https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.http.httpcontext?view=aspnetcore-6.0)

##################################################################################################

### To find the difference between Struct and Normal Classes _____

