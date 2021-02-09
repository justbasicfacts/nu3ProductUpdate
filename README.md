# nu3ProductUpdate
nu3 product update task

### Usage
git clone > build > run

client dependencies must be restore on first build 

If you see a blank page please rebuild the project, or run build-client script on terminal (npm run build-client)

### APIs

You should be authenticated via GitHub in order to use below APIs. 

#### files api  - /files

/get -> returns all file informations

/get/:fileId -> returns  file matching given fileId

/put -> adds new file

/delete/:fileId -> deletes file matching given fileId

#### products api  - /products

/get -> returns all products

/get/:handle -> returns products matching given handle

/put/:handle -> updates products matching given handle

#### inventories api - /inventory

/get -> returns inventory information

### Testing
For api tests please use nu3ProductUpdate.Tests project

For client side tests please run test-client script on terminal (in root folder: npm run test-client,  or in wwwroot folder: npm run test) 

You can find test coverage file for client side in wwwroot\coverage folder.

### Screenshots
#### Before Login
![nu3-test2](https://user-images.githubusercontent.com/29313362/107345415-94621900-6ad4-11eb-803e-1886794ec669.PNG)

#### After Login
![nu3-test3](https://user-images.githubusercontent.com/29313362/107345409-93c98280-6ad4-11eb-90bb-9d1c1e10a221.PNG)

