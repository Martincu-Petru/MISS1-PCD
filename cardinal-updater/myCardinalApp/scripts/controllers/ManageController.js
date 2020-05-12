import ContainerController from '../../cardinal/controllers/base-controllers/ContainerController.js';

var model = {
    books: []
}

var bindObject = {
    bookName: {
        label: "Book Title",
        name: "bookName",
        readonly: true,
        value: ''
    },
    author: {
        label: "Author",
        name: "author",
        readonly: true,
        value: ''
    },
    isbn: {
        label: "ISBN",
        name: "isbn",
        readonly: true,
        value: ''
    },
    pageNumber: {
        label: "Number of pages",
        name: "pageNumber",
        readonly: true,
        value: ''
    },
    category: {
        label: "Category",
        name: "category",
        readonly: true,
        value: ''
    },
    fileSize: {
        label: "File Size",
        name: "fileSize",
        readonly: true,
        value: ''
    },
    publishingHouse: {
        label: "Publishing House",
        name: "publishingHouse",
        readonly: true,
        value: ''
    }
}

export default class NewBookController extends ContainerController {
    constructor(element) {
        super(element);
        
        var xhttp = new XMLHttpRequest();

        model = {
            books: []
        }

        xhttp.open("GET", "http://localhost:3000/api/book");
        xhttp.setRequestHeader("Content-Type", "application/json");

        self = this;
        xhttp.onload = function() {
            if (this.readyState === 4 && this.status == 200) {
                var arr = JSON.parse(this.responseText);
                debugger;
                arr.forEach(book => {
                    let bookToPush = bindObject;
                    bookToPush.author.value = book.author;
                    bookToPush.bookName.value = book.name;
                    bookToPush.isbn.value = book.isbn;
                    bookToPush.pageNumber.value = book.pageNumber;
                    bookToPush.publishingHouse.value = book.publishingHouse;
                    bookToPush.fileSize.value = book.fileSize;
                    bookToPush.category.value = book.category;
                    model.books.push(bookToPush);
                });

                self.model = self.setModel(JSON.parse(JSON.stringify(model)));
            }
        }

        xhttp.send();

        let deleteBook = (isbn) => {
            console.log(this.model.getChainValue(isbn.data));

        }

        this.on("deleteBook", deleteBook, true);
    }
}