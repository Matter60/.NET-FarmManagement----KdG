const saveButton = document.getElementById("buttonSave");
if (saveButton) {
    saveButton.addEventListener('click', updateSize);
}
function updateSize() {
    const newSize = document.getElementById("inputSize").value;
    const farmId = document.getElementById("farmId").value;


    if (isNaN(newSize) || newSize <= 0) {
        alert("The size in hectares should be atleast 0.01.");
        return
    }
    
    fetch(`/api/Farms/${farmId}`, {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({sizeInHectares: parseFloat(newSize)})
    })
        .then(res => {
           if(res.ok) {
               alert("Successfully updated size");
           } else {
               alert(`Server status: ${res.status}`);
           }
           
        })
        .catch(error => {
            alert("Unexpected error occurred.");
        });
}