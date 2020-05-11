import ContainerController from '../../cardinal/controllers/base-controllers/ContainerController.js';

var model = {
    books: [{
        bookName: {
            label: "Povestea Povestilor",
            name: "bookName",
            readonly: true,
            value: "Test"
        },
        author: {
            label: "Author",
            name: "author",
            readonly: true,
            value: "Test"
        },
        isbn: {
            label: "ISBN",
            name: "isbn",
            readonly: false,
            value: "Test"
        }
    }
    ]
}

export default class HomeController extends ContainerController {
    constructor(element) {
        super(element);
        this.model = this.setModel(JSON.parse(JSON.stringify(model)));
        debugger;

        let customSubmit = () =>{
			let name = this.model.getChainValue("books")[0].bookName.value;
			let email = this.model.getChainValue("books")[0].author.value;
			let age = this.model.getChainValue("books")[0].isbn.value;
			alert(`Submitted:[${name},${email},${age}]`)
        };

        this.on("custom-submit", customSubmit, true);
    }

    
}