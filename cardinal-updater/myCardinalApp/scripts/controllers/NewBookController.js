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

        let addFile = (event) => {
            var file = event.data[0];

            this.model.fileName.value = file.name;
            this.model.fileExtension.value = file.name.split(".").pop();
            this.model.fileSize.value = file.size;
        }

        let uploadFile = () => {
            let fileModel = {
                bookName : this.model.bookName.value,
                author : this.model.author.value,
                isbn : this.model.isbn.value,
                pageNumber : this.model.pageNumber.value,
                category : this.model.category.value,
                fileSize : this.model.fileSize.value,
                publishingHouse : this.model.publishingHouse.value,
                fileName : this.model.fileName.value,
                fileExtension : this.model.fileExtension.value
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
        this.on("addFile", addFile, true);
    }
}