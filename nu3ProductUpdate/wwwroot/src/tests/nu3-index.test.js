import { html, fixture, expect, aTimeout } from "@open-wc/testing";
import sinon from "sinon";
import fetchMock from "fetch-mock/esm/client";
import {
    deleteFileSuccessfulMock,
    authenticationMock,
    uploadFilesMock,
    getFilesMock,
    file
} from "./mocks/mock-data.js";
import "../nu3-index.js";

const populateFileListForInput = (fileUploadInput) => {
    const list = new DataTransfer();
    list.items.add(file);
    const myFileList = list.files;

    fileUploadInput.files = myFileList;
};

describe("nu3-index", () => {
    it("Signin button and sign in message visible when user is not authenticated", async () => {
        const el = await fixture(html`<nu3-index></nu3-index>`);
        await aTimeout(10);
        expect(
            el.shadowRoot.querySelector(".sign-in-form > .link-button").innerText
        ).to.equal("Sign in Using GitHub");

        expect(el.authenticated).to.be.equal(false);
    });

    it("Signout button visible, user name and files rendered when user authenticated", async () => {
        fetchMock.post("authentication/check", authenticationMock);
        fetchMock.get("files", getFilesMock);

        const el = await fixture(html`<nu3-index></nu3-index> `);
        await aTimeout(10);
        expect(el.authenticated).to.be.equal(true);

        expect(
            el.shadowRoot.querySelector(".sign-in-form > .link-button").innerText //first button is the sign-in button
        ).to.equal("Sign out");

        expect(
            el.shadowRoot.querySelector(".sign-in-form > .username").innerText
        ).to.equal(authenticationMock.name);

        fetchMock.restore();
    });

    it("Signout button visible, user name and files rendered when user authenticated", async () => {
        fetchMock.post("authentication/check", authenticationMock);
        fetchMock.get("files", getFilesMock);

        const el = await fixture(html`<nu3-index></nu3-index> `);
        await aTimeout(10);
        expect(el.authenticated).to.be.equal(true);

        expect(
            el.shadowRoot.querySelector(".sign-in-form > .link-button").innerText //first button is the sign-in button
        ).to.equal("Sign out");

        expect(
            el.shadowRoot.querySelector(".sign-in-form > .username").innerText
        ).to.equal(authenticationMock.name);

        fetchMock.restore();
    });

    it("Result of the uploaded file successfully added to files list", async () => {
        fetchMock.post("authentication/check", authenticationMock);
        fetchMock.get("files", getFilesMock);
        fetchMock.put("files", uploadFilesMock);

        const el = await fixture(html`<nu3-index></nu3-index> `);
        await aTimeout(10);
        expect(el.authenticated).to.be.equal(true);
        const fileUploadInput = el.shadowRoot.querySelector("#file-upload");

        populateFileListForInput(fileUploadInput);

        const mockFileInputEvent = new Event("change", {});

        fileUploadInput.dispatchEvent(mockFileInputEvent);
        await aTimeout(10);

        expect(el.items.length).to.be.equal(3);
        expect(el.items[0].filename).to.be.equal(uploadFilesMock.fileInfo.filename);
        fetchMock.restore();
    });

    it("Empty file input raises error", async () => {
        fetchMock.post("authentication/check", authenticationMock);
        fetchMock.get("files", getFilesMock);
        fetchMock.put("files", uploadFilesMock);

        const el = await fixture(html`<nu3-index></nu3-index> `);
        await aTimeout(10);
        expect(el.authenticated).to.be.equal(true);
        const fileUploadInput = el.shadowRoot.querySelector("#file-upload");
        const mockFileInputEvent = new Event("change", {});

        fileUploadInput.dispatchEvent(mockFileInputEvent);
        await aTimeout(10);

        expect(el.items.length).to.be.equal(2);
        expect(el.hasError).to.be.true;
        fetchMock.restore();
    });

    it("Raises error on failed response", async () => {
        fetchMock.post("authentication/check", authenticationMock);
        fetchMock.get("files", getFilesMock);
        fetchMock.put("files", 500);

        const el = await fixture(html`<nu3-index></nu3-index> `);
        await aTimeout(10);
        expect(el.authenticated).to.be.equal(true);
        const fileUploadInput = el.shadowRoot.querySelector("#file-upload");

        populateFileListForInput(fileUploadInput);

        const mockFileInputEvent = new Event("change", {});

        fileUploadInput.dispatchEvent(mockFileInputEvent);
        await aTimeout(10);

        expect(el.items.length).to.be.equal(2);
        expect(el.hasError).to.be.true;
        fetchMock.restore();
    });

    it("File list updates successfully after deleting an item", async () => {
        fetchMock.post("authentication/check", authenticationMock);
        fetchMock.get("files", getFilesMock);
        fetchMock.delete(`files/${getFilesMock[0].id}`, deleteFileSuccessfulMock); //mock first item delete

        const el = await fixture(html`<nu3-index></nu3-index> `);
        await aTimeout(10);
        expect(el.authenticated).to.be.equal(true);
        const deleteButton = el.shadowRoot.querySelector("[button-role='delete']"); //get first delete button
        deleteButton.click();
        await aTimeout(10);
        expect(el.items.length).to.be.equal(1);

        fetchMock.restore();
    });

    it("Can redirect for showing raw data", async () => {
        fetchMock.post("authentication/check", authenticationMock);
        fetchMock.get("files", getFilesMock);

        const el = await fixture(html`<nu3-index></nu3-index> `);
        await aTimeout(10);

        const windowOpenStub = sinon.stub(window, "open");
        const showRawDataButton = el.shadowRoot.querySelector(
            "[button-role='show-raw']"
        ); //get first show raw data button
        showRawDataButton.click();

        expect(windowOpenStub).to.have.been.called;
    });
});