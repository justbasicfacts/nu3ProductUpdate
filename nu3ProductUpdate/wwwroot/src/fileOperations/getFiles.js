export const getFiles = async (successCallback, errorCallback) => {
    await fetch(`/files`, { method: "GET" })
        .then((response) => response.json())
        .then((success) => successCallback(success))
        .catch((error) => errorCallback(error));
};