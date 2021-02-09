import { expect } from "@open-wc/testing";
import fetchMock from "fetch-mock/esm/client";
import sinon, { assert } from "sinon";
import {
    deleteFileSuccessfulMock,
    authenticationMock,
    uploadFilesMock,
    getFilesMock,
    file,
} from "./mocks/mock-data.js";
import {
    uploadFile,
    deleteFile,
    returnFileSize,
    getFiles,
} from "../fileOperations/index.js";

describe("nu3-index", () => {
    it("returnFileSize can generate correct file sizes", async () => {
        const oneByteFileSize = returnFileSize(1);
        const twoKiloByteFileSize = returnFileSize(2048);
        const oneMegaByteFileSize = returnFileSize(1024 * 1024);
        expect(oneByteFileSize).to.equal("1 bytes");
        expect(twoKiloByteFileSize).to.equal("2.0 KB");
        expect(oneMegaByteFileSize).to.equal("1.0 MB");
    });

    it("getFiles can fetch file successfully", async () => {
        fetchMock.restore();
        fetchMock.get("files", getFilesMock);

        const success = sinon.spy();
        const error = sinon.spy();

        await getFiles(success, error);
        assert.calledOnce(success);

        sinon.reset();
    });

    it("getfiles errorCallback calling on  error", async () => {
        fetchMock.restore();
        fetchMock.get("files", 500);
        const success = sinon.spy();
        const error = sinon.spy();

        await getFiles(success, error);
        assert.calledOnce(error);

        sinon.reset();
    });

    it("deleteFile can delete file successfully", async () => {
        fetchMock.restore();
        fetchMock.delete("files/test", deleteFileSuccessfulMock);

        const success = sinon.spy();
        const error = sinon.spy();

        await deleteFile("test", success, error);
        assert.calledOnce(success);

        sinon.reset();
    });

    it("deleteFile errorCallback calling on  error", async () => {
        fetchMock.restore();
        fetchMock.delete("files/test", 500);
        const success = sinon.spy();
        const error = sinon.spy();

        await deleteFile("test", success, error);
        assert.calledOnce(error);

        sinon.reset();
    });

    it("uploadFile can upload file successfully", async () => {
        fetchMock.restore();
        fetchMock.put("files", uploadFilesMock);

        const success = sinon.spy();
        const error = sinon.spy();
        const formElement = document.createElement("form");
        await uploadFile(formElement, file, success, error);
        assert.calledOnce(success);

        sinon.reset();
    });

    it("uploadFile errorCallback calling on  error", async () => {
        fetchMock.restore();
        fetchMock.put("files", 500);

        const success = sinon.spy();
        const error = sinon.spy();
        const formElement = document.createElement("form");
        await uploadFile(formElement, file, success, error);
        assert.calledOnce(error);

        sinon.reset();
    });
});