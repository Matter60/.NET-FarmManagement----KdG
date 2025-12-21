loadContent();

    
const addForm = document.getElementById("btn-submit");
addForm.addEventListener("click", addFarmToAnimal);

function loadContent() {
    const animalId = document.getElementById("currentAnimalId").value;
    
    getLinkedFarms(animalId);
    getAvailableFarms(animalId);
}


function getLinkedFarms(animalId) {
    fetch(`/api/Animals/${animalId}/farms`, {
        method: 'GET',
        headers: { 'Accept': 'application/json' }
    })
        .then(res => {
            if (res.ok) {
                return res.json()
            } else {
                alert(`Server status: ${res.status}`)
            }
        })
        .then(data => {
            updateLinkedFarmsTable(data);
        })
        .catch(error => {
            alert("Unexpected error occurred.");
        });
}


function updateLinkedFarmsTable(farms) {
    const tableBody = document.getElementById('farmsTableBody');
    tableBody.innerHTML = ''; 

    if (farms.length === 0) {
        tableBody.innerHTML = '<tr><td colspan="5">Nog geen boerderijen gekoppeld.</td></tr>';
    }

    for (const farm of farms) {
        const rowHtml = `
            <tr>
                <td>${farm.name}</td>
                <td>${farm.location}</td>
                <td>${farm.sizeInHectares !== null ? farm.sizeInHectares : 'Unknown'}</td>
                <td>${farm.establishedYear}</td>
            </tr>
        `;
        tableBody.insertAdjacentHTML('beforeend', rowHtml);
    }
}


function getAvailableFarms(animalId) {
    fetch(`/api/Animals/${animalId}/farms-candidates`, {
        method: 'GET',
        headers: { 'Accept': 'application/json' }
    })
        .then(res => {
            if (res.ok) {
                return res.json()
            } else {
                alert(`Server status: ${res.status}`)
            }
        })
        .then(data => {
          
            updateAvailableFarmsDropdown(data);
        })
        .catch(error => {
            alert("Unexpected error occurred.");
        });
}


function updateAvailableFarmsDropdown(farmList) {
    const selectBox = document.getElementById("farmSelect");
    
    selectBox.innerHTML = '<option value="" disabled selected> --- Select a farm --- </option>';

    if (farmList.length === 0) {
        selectBox.insertAdjacentHTML('beforeend',
            '<option disabled>Geen boerderijen meer beschikbaar</option>');
    } else {
        farmList.forEach(farm => {
            const optionHtml = `<option value="${farm.id}">${farm.name}</option>`;
            selectBox.insertAdjacentHTML('beforeend', optionHtml);
        });
    }
}

function addFarmToAnimal() {
    
    
    const animalIdInput = document.getElementById("currentAnimalId");
    const farmSelect = document.getElementById("farmSelect");
    const countInput = document.getElementById("countInput");
    
    if (!farmSelect.value) {
        alert("Selecteer eerst een boerderij!");
    }
    if (!countInput.value || parseInt(countInput.value) < 1) {
        alert("Het aantal moet minimaal 1 zijn!");
    }
    
    const newFarmAnimal = {
        animalId: parseInt(animalIdInput.value),
        farmId: parseInt(farmSelect.value),
        count: parseInt(countInput.value)
    };

   
    fetch('/api/FarmAnimals', {
        method: 'POST',
        body: JSON.stringify(newFarmAnimal),
        headers: {
            'Content-Type': 'application/json', 
            'Accept': 'application/json'
        }
       
    })
        .then(res => {
            if (res.ok) {
                farmSelect.value = ""; 
                countInput.value = 1;  
                
                loadContent();
            } else {
                alert("Er is iets misgegaan bij het leggen van de relatie.");
            }
        })
        .catch(error => {
            alert("Er heeft zich een onverwachte fout voorgedaan.");
        });
}

