import {
    uploadFile,
    deleteFile,
    returnFileSize,
    getFiles,
} from "./fileOperations/index.js";
import { nu3IndexStyle } from "./style/nu3IndexStyle.js";
import cloudUpload from "./images/cloud-upload.js";
import { LitElement, html } from "lit-element";

export class Nu3Index extends LitElement {
    static get styles() {
        return nu3IndexStyle;
    }

    static get properties() {
        return {
            items: { type: Object },
            authenticated: { type: Boolean },
            userName: { type: String },
            authenticationCheckCompleted: { type: Boolean },
        };
    }

    constructor() {
        super();
        this.items = [];
        this.authenticated = false;

        this.checkAuthentication().then(() => {
            if (this.authenticated) {
                getFiles(this.onFilesFetched.bind(this), this.errorCallback.bind(this));
            }
        });
    }

    checkAuthentication() {
        return fetch("/authentication/check", { method: "POST" })
            .then((response) => {
                if (response.ok) {
                    return response.json();
                } else {
                    throw new Error("User is not authenticated");
                }
            })
            .then((data) => {
                this.userName = data.name;
                this.authenticated = true;
                this.authenticationCheckCompleted = true;
            })
            .catch(() => {
                this.userName = "";
                this.authenticationCheckCompleted = true;
            });
    }

    render() {
        return html`
      ${this.authenticationCheckCompleted
                ? html` <h1>nu3 Product Management</h1>
            <hr />
            <div class="sign-in-container">
              <form
                class="sign-in-form"
                action="${!this.authenticated
                        ? "/authentication/signin"
                        : "/authentication/signout"}"
                method="POST"
              >
                <input value="GitHub" name="provider" type="hidden" />${this
                        .authenticated
                        ? html`Hello! <span class="username">${this.userName}</span>`
                        : ""}

                <button class="link-button">
                  ${!this.authenticated ? "Sign in Using GitHub" : "Sign out"}
                </button>
              </form>
              ${this.authenticated
                        ? html`<button
                      class="link-button"
                      href="/files"
                      @click=${(e) => this.showRaw(e)}
                      button-role="show-raw"
                    >
                      Show Files (Raw)
                    </button>
                    <button
                      class="link-button"
                      href="/products"
                      @click=${(e) => this.showRaw(e)}
                      button-role="show-raw"
                    >
                      Show Products (Raw)
                    </button>
                    <button
                      class="link-button"
                      href="/inventory"
                      @click=${(e) => this.showRaw(e)}
                      button-role="show-raw"
                    >
                      Show Inventory (Raw)
                    </button>`
                        : ``}
            </div>
            <hr />

            ${this.authenticated
                        ? this.getAuthenticatedTemplate()
                        : `Please sign in to upload and see the files.`}`
                : "Loading"}
    `;
    }

    onFileUploaded(item) {
        this.items = [item.fileInfo, ...this.items];
    }

    onFileDeleted(result) {
        if (result.isDeleted) {
            this.items = this.items.filter((item) => item.id != result.id);
        }
    }

    onFilesFetched(result) {
        this.items = result;
    }

    getAuthenticatedTemplate() {
        return html` <form>
        <label for="file-upload" class="file-upload-label">
          Upload Products or Inventory File

          <div class="upload-image">${cloudUpload}</div>
        </label>

        <input
          id="file-upload"
          class="file-upload-input"
          type="file"
          accept=".xml, .csv"
          @change=${this.upload}
        />
      </form>

      ${this.items && this.items.length > 0
                ? html`
            <div class="file-info-row header-row">
              <span>File Type</span>
              <span>Upload Date</span>
              <span>File Size</span>
              <span></span>
              <span></span>
            </div>
            ${this.items.map(
                    (item) =>
                        html`<div class="file-info-row">
                  <span class="file-detail">
                    ${item.mimeType === "text/xml"
                                ? "Products"
                                : "Inventory"}</span
                  ><span class="file-detail">
                    ${Intl.DateTimeFormat("de-DE", {
                                    dateStyle: "medium",
                                    timeStyle: "medium",
                                }).format(new Date(item.uploadDate))}</span
                  >
                  <span class="file-detail">
                    ${returnFileSize(item.length)}</span
                  >
                  <span class="file-detail">
                    <a
                      class="link-button"
                      href="/files/${item.id}"
                      target="_blank"
                    >
                      Download
                    </a></span
                  >
                  <span>
                    <a
                      class="link-button"
                      href="#"
                      button-role="delete"
                      data-item-id="${item.id}"
                      @click=${this.delete}
                      >Delete</a
                    ></span
                  >
                </div> `
                )}
          `
                : ``}`;
    }

    upload(e) {
        e.preventDefault();

        const uploadFileInput = e.target;
        const file = uploadFileInput?.files[0];
        if (file) {
            uploadFile(
                e.target.form,
                file,
                this.onFileUploaded.bind(this),
                this.errorCallback.bind(this)
            );
        } else {
            this.hasError = true;
        }

        uploadFileInput.value = "";
    }

    delete(e) {
        e.preventDefault();

        const eventTarget = e.currentTarget;
        if (eventTarget) {
            const itemId = eventTarget.getAttribute("data-item-id");
            if (itemId) {
                deleteFile(
                    itemId,
                    this.onFileDeleted.bind(this),
                    this.errorCallback.bind(this)
                );
            }
        }
    }

    showRaw(e) {
        e.preventDefault();
        window.open(e.target.getAttribute("href"), "_blank");
    }

    errorCallback(error) {
        this.hasError = true;
        this.errorMessage = error;
    }
}

window.customElements.define("nu3-index", Nu3Index);