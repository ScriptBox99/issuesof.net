using System;

using IssuesOfDotNet.Querying;

namespace IssuesOfDotNet.Pages
{
    public sealed partial class Query
    {
        private string _text;

        public string Text
        {
            get => _text;
            set
            {
                if (_text != value)
                {
                    _text = value;
                    Update();
                }
            }
        }

        public string Output { get; set; }

        private void Update()
        {
            var query = QuerySyntax.Parse(_text);
            var boundQuery = BoundExpression.Create(query);

            Output = query + Environment.NewLine + "-----" + Environment.NewLine + boundQuery;
        }
    }
}
