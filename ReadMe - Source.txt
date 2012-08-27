Source Code Documentation

Contains helpful details on the implementation.


InTheBoks\Commands\
- Within here all the commands are added. They are all in the same namespace. Organize all commands in folders.

InTheBoks\Handlers\
- Handlers for all the commands. They are all in the same namespace. Organize all helpers in folders.

InTheBoks.Web\Api\
- All the Api controllers. This is where permission control is handled. Always verify if items belongs
to the specified user. Handlers shouldn't care about permissions and trust the api/presentation-layer.

