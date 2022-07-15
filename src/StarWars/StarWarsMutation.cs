using GraphQL;
using GraphQL.Types;
using StarWars.Types;

namespace StarWars
{
    /// <example>
    /// This is an example JSON request for a mutation
    /// {
    ///   "query": "mutation ($human:HumanInput!){ createHuman(human: $human) { id name } }",
    ///   "variables": {
    ///     "human": {
    ///       "name": "Boba Fett"
    ///     }
    ///   }
    /// }
    /// </example>
    public class StarWarsMutation : ObjectGraphType
    {
        public StarWarsMutation(StarWarsData data)
        {
            Name = "Mutation";

            Field<HumanType>(
                "createHuman",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<HumanInputType>> {Name = "human"}
                ),
                resolve: context =>
                {
                    var human = context.GetArgument<Human>("human");
                    return data.AddHuman(human);
                });

            Field<BooleanGraphType>(
                "deleteHuman",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "id", Description = "Id of the human to remove." }
                ),
                resolve: context =>
                {
                    data.DeleteHuman(context.GetArgument<string>("id"));
                    return true;
                });
        }
    }
}
