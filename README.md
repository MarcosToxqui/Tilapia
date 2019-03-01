# DotNet.Tilapia

##What is Tilapia

Tilapia is a simple library that provides a set of extension methods to made common actions as compariosions of *null*, logic comparions usually maked with operator **>** or **<**.

Additionally, Tilapia provides a simple code that intent simplify to write code for access to Sql Server databases.

I hope that this library (available as nuget) help you.

##Data

Initialize instance for the `DbContext`, is your database access point.

```cs
static DbContext context = DbContext.PrepareContext("connection_string_config_key_name");
```

###`Query` object

Is the main object to set up the SQL especs that you wish to execute with SqlServer.

|Property|Description|
|-|-|
|`string QueryText`|The SQL statement text|
|`List<SqlParameter> Parameters`|List of SqlParameters for parameterized  SQL statement.|
|`bool WithTransaction`|Set up if execution should execute a transaction.|
|`CommandType TextCommandType`|Setup the command type: Text or StoredProcedure|

###`DbContext` object

Is the context used to access to the database.

|Methods|Description|
|-|-|
|`Query Query`|Gets or set the Query instance with the specs.|
|`ExecuteQuery`|Finally executes the query|

## Usage example

```cs
var insert = Query.CreateQuery();
    insert.QueryText = "INSERT INTO [dbo].[foo] ([FooId],@Category,@Tags,@Status)";
    insert.Parameters = new List<SqlParameter>() {
                            new SqlParameter("@FooId", 1),
                            new SqlParameter("@Category", 1),
                            new SqlParameter("@Tags", "A, B, C"),
                            new SqlParameter("@Status", true)
                            };

    insert.WithTransaction = true;
    insert.TextCommandType = CommandType.Text;
    insert.IsolationLevel = IsolationLevel.ReadCommitted;

    context.Query = insert;
    context.ExecuteQuery();
```

###Common extensions

|Function name|Extends to|
|-|-|
|```bool IsNullOrEmpty```|**string**|
|```bool IsNullOrEmpty<T>```|**IList** based object|
|```bool IsNullOrEmpty<T>```|**IEnumerable** bassed objects|
|```bool IsNullOrEmpty```|**Array**|
|```bool IsNullOrEmpty<T>```|**object[]** Common array|
|```bool IsNullOrEmpty<T, U>```|**IDictionary** based object|
|```bool IsNull```|**object**|
|```bool IsNotNull```|**object**|
|```string Join(string)```|**string[]**|
|```string Join(char)```|**string[]**|
|```string Join(string)```|**List<>** of string|
|```string Join(string)```|**Enumerable<>** of string|
|```byte[] GetBytes```|**string**|
|```string GetStringForm```|**byte[]**|
|```string ToBase64```|**string**|
|```string CastFromBase64```|**string** (base64 string)|
|```IList<T> RemoveNulls<T>```|**IList** based object|
|```IEnumerable<T> RemoveNulls<T>```|**IEnumerable**|
|```string RemoveRedundantWhiteSpaces```|**string**|
|```string Capitalize```|**string**|
|```string RemoveNumbers```|**string**|
|```bool IsValidMailAddress```|**string**|
|```bool IsValidUrl```|**string**|

###Comparision extensions

|Function name|Extends to|
|-|-|
|```bool IsEqual<T1, T2>```|**T** type|
|```bool IsLessThan```|**int**|
|```bool IsLessThan```|**float**|
|```bool IsLessThan```|**double**|
|```bool IsLessThan```|**decimal**|
|```bool IsBiggerThan```|**int**|
|```bool IsBiggerThan```|**float**|
|```bool IsBiggerThan```|**double**|
|```bool IsBiggerThan```|**decimal**|

##Licence

Copyright 2019 Marcos Toxqui

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.