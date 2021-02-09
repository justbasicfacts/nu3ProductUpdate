import { css } from "lit-element";

export const nu3IndexStyle = css`
  :host {
    display: block;
    width: 100%;
  }

  .link-button {
    background: #ffffff none repeat scroll 0 0;
    border: thin solid gray;
    color: black;
    outline: medium none;
    padding: 4px;
    text-decoration: none;
  }

  .link-button:hover {
    background: rgb(246, 246, 246);
    cursor: pointer;
  }

  .file-upload-input {
    display: none;
  }

  .file-upload-label {
    display: flex;
    border: thin solid gray;
    width: 300px;
    padding: 15px;
    cursor: pointer;
  }

  .file-upload-label:hover {
    background-color: rgb(246, 246, 246);
  }

  .file-detail {
    margin-right: 8px;
  }

  .upload-image {
    width: 30px;
    margin-left: auto;
  }

  .file-info-row {
    display: flex;
    align-items: flex-start;
    justify-content: space-between;
    width: 800px;
    padding: 8px;
  }

  .file-info-row > * {
    flex: 1;
    margin-left: 2%;
    min-width: 0;
  }

  .file-info-row > *:first-child {
    margin-left: 0;
  }

  .sign-in-container {
    padding-top: 12px;
    padding-bottom: 12px;
    border-bottom: thin solid lightgray;
    border-top: thin solid lightgray;
    margin-bottom: 8px;
  }

  .header-row {
    font-weight: bold;
  }

  .raw-data-buttons-container {
    margin-top: 12px;
  }

  .sign-in-form {
    display: inline-block;
  }

  .username {
    font-weight: bold;
    margin-right: 20px;
  }
`;