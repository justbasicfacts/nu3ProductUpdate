export const deleteFile = async (id, successCallback, errorCallback) => {
    await fetch(`/files/${id}`, {
        method: "DELETE",
    })
        .then((response) => response.json())
        .then((success) => successCallback(success))
        .catch((error) => errorCallback(error));
};