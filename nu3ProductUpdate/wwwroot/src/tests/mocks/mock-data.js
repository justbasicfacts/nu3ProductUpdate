export const file = new File(["foo"], "foo.txt", {
    type: "text/plain",
});

export const deleteFileSuccessfulMock = {
    id: "test",
    isDeleted: true,
};

export const authenticationMock = { name: "test" };

export const uploadFilesMock = {
    fileInfo: {
        id: "test-uploaded-mock",
        filename: "test-uploaded-mock.xml",
        mimeType: "text/xml",
        length: 1,
        chunks: 1,
        uploadDate: "2021-02-08T14:40:31.7537195+03:00",
        metadata: {},
    },
    isSuccessful: true,
    fileType: 0,
};

export const getFilesMock = [
    {
        id: "test",
        filename: "test",
        mimeType: "text/xml",
        length: 1,
        chunks: 1,
        uploadDate: "2021-02-07T21:50:32.629+03:00",
        metadata: {},
    },
    {
        id: "test2",
        filename: "test2",
        mimeType: "text/csv",
        length: 1,
        chunks: 1,
        uploadDate: "2021-02-07T21:50:32.629+03:00",
        metadata: {},
    },
];