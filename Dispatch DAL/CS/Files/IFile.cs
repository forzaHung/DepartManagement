using System.Collections.Generic;

namespace Dispatch
{
    public interface IFile
	{
		long Insert(FileEntity files);
        bool Update(FileEntity files);
		bool Delete(long id);
		List<FileEntity> ListAll();
		List<FileEntity> ListAllPaging(string searchCode, string searchName, string searchUploadDate, int pageIndex, int pageSize, ref int totalRow);
		FileEntity ViewDetail(long id);
        FileEntity ViewDetailByDispatch(long DispatchId,bool Type);
    }
}
