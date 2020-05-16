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

var booksToSet;

var bindBooks = function(arr) {
    console.log(arr);

    model = {
        books: []
    }

    arr.forEach(book => {
        let bookToPush = Object.assign({}, bindObject);
        bookToPush.author.value = book.author;
        bookToPush.bookName.value = book.bookName;
        bookToPush.isbn.value = book.isbn;
        bookToPush.pageNumber.value = book.pageNumber;
        bookToPush.publishingHouse.value = book.publishingHouse;
        bookToPush.fileSize.value = book.fileSize;
        bookToPush.category.value = book.category;
        model.books.push(bookToPush);
    });

    booksToSet = JSON.parse(JSON.stringify(model));
}

function getBooks(callback) {
    var xhttp = new XMLHttpRequest();

    xhttp.open("GET", "http://localhost:3000/api/books");
    xhttp.setRequestHeader("Content-Type", "application/json");
    xhttp.send();

    xhttp.onreadystatechange = function() {
        if (this.readyState === 4 && this.status == 200) {
            var arr = JSON.parse(this.responseText);
            callback.apply(this, [arr]);
        }
    }
}

export default class HomeController extends ContainerController {
    constructor(element) {
        super(element);

        getBooks(bindBooks);

        this.model = this.setModel(booksToSet);
    }
}