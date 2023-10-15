# IdProvider
A simple rest API that provides incremental int id

# Status
It is still at a very early stage.

# Install
- Download and compile with .net 7
- Create manually the wwwroot folder

# Endpoints
## Default access
http(s)/url/api/v1/Ids

## Post

{
    Prefix="EXAMPLE-",
    InitialId=12
}

## Get

**GetNewIdByPrefix** 
/api/v1/Ids/next

Returns the next id of the given prefix

**GetCurrentIdByPrefix**
/api/v1/Ids/current

Returns the current id of the given prefix