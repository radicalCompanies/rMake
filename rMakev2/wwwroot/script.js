
function focusInput() {
    document.getElementById("ProjectName").select();
}

function focusLastTextEditor(id) {
    
    document.getElementById("Area" + id-1);
    const parent = document.querySelector('#Area'+id);   
    const child1 = parent.querySelector('.rte-editor'); 
    const child2 = child1.querySelectorAll('.ql-editor');
    child2[0].focus();

 
}






//https://www.w3schools.com/js/js_htmldom_nodelist.asp