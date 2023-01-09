
function focusInput() {
    document.getElementById("ProjectName").select();
}

function focusLastTextEditor(id) {
    
    document.getElementById("Area" + id-1);

    const parent = document.querySelector('#Area'+id);
    console.log("1"+parent); 

    const child1 = parent.querySelector('.rte-editor');
    console.log("2" + child1); 

    const child2 = child1.querySelectorAll('.ql-editor');
    console.log(child2); 
    

    const child3 = child2.document.querySelectorAll("p");
    console.log(child3); 
    child3.focus();
}




//https://www.w3schools.com/js/js_htmldom_nodelist.asp