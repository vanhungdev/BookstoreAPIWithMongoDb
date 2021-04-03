using FluentValidation;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bookstore.Infrastructure.Entities
{
    public class Category : Base
    {
        [BsonElement("Name")]
        public string Name { get; set; }
        public string Slug { get; set; }
        public int Parentid { get; set; }
        public int Orders { get; set; }
        public string Metakey { get; set; }
        public string Metadesc { get; set; }
        public DateTime Created_at { get; set; }
        public int Created_by { get; set; }
        public DateTime Updated_at { get; set; }
        public int Updated_by { get; set; }
        public int Status { get; set; }
    }

    public class categoryValidator : AbstractValidator<Category>
    {
        public categoryValidator()
        {
            RuleFor(x => x.Name).Length(0, 255);
            RuleFor(x => x.Parentid).GreaterThan(-1);
            RuleFor(x => x.Orders).GreaterThan(0);
        }
    }
}
