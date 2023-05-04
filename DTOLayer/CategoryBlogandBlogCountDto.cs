using CoreLayer.Entities;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOLayer
{
    public class CategoryBlogandBlogCountDto: IDto
    {
        public Category Category { get; set; }
        public int CategoryBlogCount { get; set; }
    }
}
