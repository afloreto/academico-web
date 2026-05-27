namespace Academico.Filters;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class IgnoreAuthFilterAttribute : Attribute { }