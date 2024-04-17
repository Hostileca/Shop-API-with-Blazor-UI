﻿
using Microsoft.AspNetCore.Http;

namespace Shop.BL.Services.Interfaces
{
    public interface IFilesService
    {
        Task<int> UploadProductImage(int productId, IFormFile file);
        Task<int> UploadCategoryImage(int categoryId, IFormFile file);
        Task<int> UploadManufacturerImage(int manufacturerId, IFormFile file);
        Task<byte[]> GetProductImageById(int imageId);
        Task<byte[]> GetCategoryImageById(int imageId);
        Task<byte[]> GetManufactuerImageById(int imageId);
        Task<int> DeleteProductImage(int imageId);
        Task<int> DeleteCategoryImage(int imageId);
        Task<int> DeleteManufacturerImage(int imageId);
    }
}
