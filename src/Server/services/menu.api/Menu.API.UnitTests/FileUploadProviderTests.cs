﻿using System.IO;
using System.Threading;
using System.Threading.Tasks;
using BaseUnitTests;
using Menu.API.Abstraction.Facades;
using Menu.API.Managers;
using Microsoft.AspNetCore.Http;
using Moq;
using Xunit;

namespace Menu.API.UnitTests
{
    public class FileUploadProviderTests : BaseAutoMockedTest<LocalFileUploadManager>
    {
		[Theory, AutoDomainData]
	    public async Task Given_file_and_uniq_id_Upload_should_copy_file_and_should_add_uploaded_data_with_uniq_id(string uniqId)
		{
			string fullPath = "wwwroot/xxx_123.png";
			string uniqFileName = "xxx_123.png";
			string uniqName = "xxx_123";

			var file = GetMock<IFormFile>();
			file.SetupGet(x => x.FileName).Returns("file.png");
			file.Setup(x => x.CopyToAsync(Stream.Null, default(CancellationToken))).Returns(Task.CompletedTask)
;
			var fileInfoFacade = GetMock<IFileInfoFacade>();
			fileInfoFacade.Setup(x => x.GetUniqName()).Returns(uniqName);
			fileInfoFacade.Setup(x => x.GetFilePathWithWebRoot(uniqFileName)).Returns(fullPath);
			fileInfoFacade.Setup(x => x.GetFileStream(fullPath, FileMode.Create)).Returns(Stream.Null);

			var provider = ClassUnderTest;

			await provider.Upload(file.Object);

			Assert.True(provider.HasFile(uniqId));
		}

        [Fact]
        public void Given_file_name_Remove_should_remove_uploaded_file_if_Exists()
        {
            // given
            var fileName = "file.png";
            GetMock<IFileInfoFacade>().Setup(x => x.Exists(fileName)).Returns(true);

            // when
            ClassUnderTest.Remove(fileName);

            // then
            GetMock<IFileInfoFacade>().Verify(x => x.Delete(fileName), Times.Once);
        }

        [Theory, AutoDomainData]
        public async Task Reset_test(string uniqId)
        {
            string fullPath = "wwwroot/xxx_123.png";
            string uniqFileName = "xxx_123.png";
            string uniqName = "xxx_123";

            var file = GetMock<IFormFile>();
            file.SetupGet(x => x.FileName).Returns("file.png");
            file.Setup(x => x.CopyToAsync(Stream.Null, default(CancellationToken))).Returns(Task.CompletedTask)
                ;
            var fileInfoFacade = GetMock<IFileInfoFacade>();
            fileInfoFacade.Setup(x => x.GetUniqName()).Returns(uniqName);
            fileInfoFacade.Setup(x => x.GetFilePathWithWebRoot(uniqFileName)).Returns(fullPath);
            fileInfoFacade.Setup(x => x.GetFileStream(fullPath, FileMode.Create)).Returns(Stream.Null);

            var provider = ClassUnderTest;

            await provider.Upload(file.Object);
        }


        [Theory, AutoDomainData]
        public async Task RemoveUploadedFileByUniqId_test(string uniqId)
        {
            string fullPath = "wwwroot/xxx_123.png";
            string uniqFileName = "xxx_123.png";
            string uniqName = "xxx_123";

            var file = GetMock<IFormFile>();
            file.SetupGet(x => x.FileName).Returns("file.png");
            file.Setup(x => x.CopyToAsync(Stream.Null, default(CancellationToken))).Returns(Task.CompletedTask);
            var fileInfoFacade = GetMock<IFileInfoFacade>();
            fileInfoFacade.Setup(x => x.GetUniqName()).Returns(uniqName);
            fileInfoFacade.Setup(x => x.GetFilePathWithWebRoot(uniqFileName)).Returns(fullPath);
            fileInfoFacade.Setup(x => x.GetFileStream(fullPath, FileMode.Create)).Returns(Stream.Null);
            fileInfoFacade.Setup(x => x.Exists(uniqFileName)).Returns(true);
            var provider = ClassUnderTest;

            await provider.Upload(file.Object);

            GetMock<IFileInfoFacade>().Verify(x => x.Delete(uniqFileName), Times.Once);
            Assert.False(provider.HasFile(uniqId));
        }
    }
}
