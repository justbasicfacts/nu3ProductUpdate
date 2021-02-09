export const uploadFile = async (form, file, successCallback, errorCallback) => {
    var formData = new FormData(form);
    formData.append("productData", file);
    await fetch("/files", {
        method: "PUT",
        body: formData,
    })
        .then((response) => response.json())
        .then((success) => successCallback(success))
        .catch((error) => errorCallback(error));
};