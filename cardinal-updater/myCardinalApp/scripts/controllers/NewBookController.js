import ContainerController from '../../cardinal/controllers/base-controllers/ContainerController.js';

var model = {
    bookName: {
        label: "Book Title",
        name: "bookName",
        value: ''
    },
    author: {
        label: "Author",
        name: "author",
        value: ''
    },
    isbn: {
        label: "ISBN",
        name: "isbn",
        value: ''
    },
    pageNumber: {
        label: "Number of pages",
        name: "pageNumber",
        value: ''
    },
    category: {
        label: "Category",
        name: "category",
        value: ''
    },
    fileSize: {
        label: "File Size",
        name: "fileSize",
        value: ''
    },
    publishingHouse: {
        label: "Publishing House",
        name: "publishingHouse",
        value: ''
    },
    fileName: {
        label: "File Name",
        name: "fileName",
        value: ''
    },
    fileExtension: {
        label: "File Extension",
        name: "fileExtension",
        value: ''
    },
    pskFileChooser: {
        label: "Choose a file",
        name: "pskFileChooser"
    }
}

export default class NewBookController extends ContainerController {
    constructor(element) {
        super(element);
        this.model = this.setModel(JSON.parse(JSON.stringify(model)));

        let uploadFile = () => {
            let fileModel = {
                bookName : this.model.getChainValue("bookName.value"),
                author : this.model.getChainValue("author.value"),
                isbn : this.model.getChainValue("isbn.value"),
                pageNumber : this.model.getChainValue("pageNumber.value"),
                category : this.model.getChainValue("category.value"),
                fileSize : this.model.getChainValue("fileSize.value"),
                publishingHouse : this.model.getChainValue("publishingHouse.value"),
                fileName : this.model.getChainValue("fileName.value"),
                fileExtension : this.model.getChainValue("fileExtension.value")
            }

            var xhttp = new XMLHttpRequest();

            xhttp.open("POST", "http://localhost:3000/api/book");
            xhttp.setRequestHeader("Content-Type", "application/json");
            xhttp.onreadystatechange = function() {
                if (this.readyState === 4 && this.status == 200) {
                    console.log(this.responseText);
                }
            }

            xhttp.send(JSON.stringify(fileModel));
        }

        this.on("uploadFile", uploadFile, true);
    }
}