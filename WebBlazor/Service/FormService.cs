using Microsoft.AspNetCore.Components;
using WebBlazor.Components.Pages.Forms.Card;
using WebBlazor.Components.Pages.Forms.Category;
using WebBlazor.Components.Pages.Forms.Manufacturer;
using WebBlazor.Components.Pages.Forms.Price;
using WebBlazor.Components.Pages.Forms.Product;

namespace WebBlazor.Components.Pages.Forms
{
    public class FormService
    {
        private Dictionary<string, Dictionary<string, RenderFragment>> ObjectActionDictionary = new Dictionary<string, Dictionary<string, RenderFragment>>
        {
            {
                "category", new Dictionary<string, RenderFragment>
                {
                    { "add", RenderFragment<CategoryCreateForm>() },
                    { "delete", RenderFragment<CategoryDeleteForm>() },
                    { "update", RenderFragment<CategoryUpdateForm>() },
                    { "imageLoad", RenderFragment<CategoryImageLoadForm>() },
                }
            },
            {
                "manufacturer", new Dictionary<string, RenderFragment>
                {
                    { "add", RenderFragment<ManufacturerCreateForm>() },
                    { "delete", RenderFragment<ManufacturerDeleteForm>() },
                    { "update", RenderFragment<ManufacturerUpdateForm>() },
                    { "imageLoad", RenderFragment<ManufacturerImageLoadForm>() },
                }
            },
            {
                "product", new Dictionary<string, RenderFragment>
                {
                    { "add", RenderFragment<ProductCreateForm>() },
                    { "delete", RenderFragment<ProductDeleteForm>() },
                    { "update", RenderFragment<ProductUpdateForm>() },
                    { "imageLoad", RenderFragment<ProductImageLoadForm>() },
                    { "imageDelete", RenderFragment<ProductImageDeleteForm>() },
                }
            },
            {
                "card", new Dictionary<string, RenderFragment>
                {
                    { "add", RenderFragment<CardCreateForm>() },
                    { "delete", RenderFragment<CardDeleteForm>() },
                }
            },
            {
                "price", new Dictionary<string, RenderFragment>
                {
                    { "update", RenderFragment<PriceUpdateForm>() },
                }
            },
        };

        public RenderFragment GetFragment(string @object, string action)
        {
            try
            {
                return ObjectActionDictionary[@object][action];
            }
            catch
            {
                return null;
            }
        }

        private static RenderFragment RenderFragment<T>()
        {
            return builder =>
            {
                builder.OpenComponent(0, typeof(T));
                builder.CloseComponent();
            };
        }
    }
}
