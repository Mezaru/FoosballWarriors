${
    // $Classes/Enums/Interfaces(filter)[template][separator]
    // filter (optional): Matches the name or full name of the current item. * = match any, wrap in [] to match attributes or prefix with : to match interfaces or base classes.
    // template: The template to repeat for each matched item
    // separator (optional): A separator template that is placed between all templates e.g. $Properties[public $name: $Type][, ]

    // More info: http://frhagn.github.io/Typewriter/


    // Enable extension methods by adding using Typewriter.Extensions.*
    using Typewriter.Extensions.Types;

    // Uncomment the constructor to change template settings.
    Template(Settings settings)
    {
        settings.OutputFilenameFactory = (file) => $"..\\..\\..\\..\\FoosballWarriors\\FoosballApp\\src\\app\\shared\\models\\{file.Name.Replace("Model.cs", ".model.ts")}";
    }

    string ModellessName(Class c)
    {
        return c.Name.Replace("Model", "");
    }

    string ModellessName(Property p)
    {
        return p.Type.Name.Replace("Model", "");
    }

    string Imports(Class c)
    {
        var names = c.Properties.Where(x => !x.Type.Namespace.Contains("System")).Select(x => $"import {{ {x.Type.Name.Replace("Model", "")} }} from 'src/app/shared/models/{x.Type.Name.Replace("Model", "")}.model'").Distinct();
        return string.Join(Environment.NewLine, names);
    }
    
    string RemoveModel(string s)
    {
        return s.Replace("Model", "");
    }
}$Classes(*Model)[$Imports

export class $ModellessName {
    $Properties[
    public $Name: $ModellessName = $Type[$Default];]

}]