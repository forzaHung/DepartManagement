using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Prototype.Code
{
    public class ImportService
    {
        private ClosedXmlReader _closedXmlReader;
        private readonly IProductRepository _productRepository;
        private readonly ISettingRepository _settingRepository;
        private readonly IUnitOfWork _unitOfWork;
        public ImportService(IProductRepository productRepository, ISettingRepository settingRepository, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _productRepository.EnableNoTracking();
            _settingRepository = settingRepository;
            _unitOfWork = unitOfWork;
        }
        //Import with settings
        public FileImportEntity ImportProducts(Stream stream, int settingId, string columnCode, string columnName, string categoryName)
        {
            var fileImport = new FileImportEntity();
            try
            {
                var settingEntity = _settingRepository.GetById(settingId);
                if (settingEntity == null)
                    return fileImport;

                _closedXmlReader = new ClosedXmlReader(stream);
                int invalidCount = 0;
                _closedXmlReader.UpdateDictMap(columnCode, columnName, categoryName);
                var productList = _closedXmlReader.ToProductList(ref invalidCount);
                fileImport.ProductCount = productList.Count;
                fileImport.FileSize = stream.Length;
                if (productList.Count > invalidCount)
                {
                    fileImport.Status = true;
                    var barcodeService = new BarcodeService(settingEntity);

                    int iSaveCount = 0;
                    foreach (var product in productList)
                    {
                        //Check exist productCode
                        product.SettingId = settingId;
                        var productExist =
                            _productRepository.Get(
                                c => c.SettingId == product.SettingId && c.ProductCode == product.ProductCode);

                        if (productExist == null || string.IsNullOrWhiteSpace(productExist.Barcode))
                        {
                            barcodeService.UpdateBarcode(product);
                            if (productExist == null)
                                _productRepository.Add(product);
                            else
                            {
                                productExist.Barcode = product.Barcode;
                                productExist.JsonData = product.JsonData;
                                productExist.Name = product.Name;
                                productExist.Status = product.Status;
                                _productRepository.Update(productExist);
                            }
                            iSaveCount++;
                            if (iSaveCount % 200 == 0)
                            {
                                _unitOfWork.Commit();
                            }
                        }
                    }
                    fileImport.SuccessCount = iSaveCount;
                    _unitOfWork.Commit();
                    return fileImport;
                }
            }
            catch (Exception ex)
            {
                //
            }
            return fileImport;
        }
    }
}