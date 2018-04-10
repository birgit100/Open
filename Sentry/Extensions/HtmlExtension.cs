using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Open.Sentry.Extensions
{
    public static class HtmlExtension
    {
        public static IHtmlContent EditingControlsFor<TModel, TResult>(this IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TResult>> expression)
        {
            var htmlStrings = new List<object>
            {
                new HtmlString("<div class=\"form-group\">"),
                htmlHelper.LabelFor(expression, new {@class = "control-label col-md-2"}),
                new HtmlString("<div class=\"col-md-10\">"),
                htmlHelper.EditorFor(expression, new {htmlAttributes = new {@class = "form-control"}}),
                htmlHelper.ValidationMessageFor(expression, "", new {@class = "text-danger"}),
                new HtmlString("</div>"),
                new HtmlString("</div>"),
            };
            return new HtmlContentBuilder(htmlStrings);
        }

        public static IHtmlContent ViewingControlsFor<TModel, TResult>
        (this IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TResult>> expression)
        {
            var htmlStrings = new List<object>
            {
                new HtmlString("<div class=\"form-group\">"),
                htmlHelper.LabelFor(expression, new {@class = "control-label col-md-2"}),
                new HtmlString("<div class=\"col-md-10\" style=\"margin-top:10px\">"),
                htmlHelper.DisplayFor(expression,
                    new {htmlAttributes = new {@class = "form-control"}}),
                new HtmlString("</div>"),
                new HtmlString("</div>")
            };
            return new HtmlContentBuilder(htmlStrings);
        }
    }
}
