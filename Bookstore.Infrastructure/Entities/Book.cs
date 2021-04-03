using FluentValidation;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bookstore.Infrastructure.Entities
{
    public class Book : Base
    {

        public int catid { get; set; }
        [BsonElement("Name")]
        public string name { get; set; }
        public string slug { get; set; }
        public string img { get; set; }
        public string detail { get; set; }
        public int number { get; set; }
        public double price { get; set; }
        public int? sold { get; set; }
        public double pricesale { get; set; }
        public string publishing { get; set; }
        public string translator { get; set; }
        public string author { get; set; }
        public string pageSize { get; set; }
        public int? pageTotal { get; set; }
        public DateTime created_at { get; set; }
        public int created_by { get; set; }
        public DateTime updated_at { get; set; }
        public int updated_by { get; set; }
        public int status { get; set; }
    }
    public class BookValidator : AbstractValidator<Book>
    {
        public BookValidator()
        {
            RuleFor(x => x.name).Length(0, 255);
            RuleFor(x => x.catid).GreaterThan(-1);
            RuleFor(x => x.number).GreaterThan(0);
        }
    }
}
