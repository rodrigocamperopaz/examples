using System;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.Types;
using StarWars.Types;

namespace StarWars
{
    public class StarWarsQuery : ObjectGraphType<object>
    {
        public StarWarsQuery(StarWarsData data)
        {
            Name = "Query";


            Field<ListGraphType<HumanType>>("humans", resolve: context => data.GetHumansAsync().Result);
            Field<ListGraphType<DroidType>>("droids", resolve: context => data.GetDroidsAsync().Result);

            FieldAsync<CharacterInterface>("hero", resolve: async context => await data.GetDroidByIdAsync("3"));
            FieldAsync<HumanType>(
                "human",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "id", Description = "id of the human" }
                ),
                resolve: async context => await data.GetHumanByIdAsync(context.GetArgument<string>("id"))
            );

            Func<IResolveFieldContext, string, Task<Droid>> func = (context, id) => data.GetDroidByIdAsync(id);

            FieldDelegate<DroidType>(
                "droid",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "id", Description = "id of the droid" }
                ),
                resolve: func
            );
        }
    }
}
