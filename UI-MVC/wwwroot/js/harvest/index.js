function init() {
    getHarvests();
    initRefresh();
}

init();
function initRefresh() {
    const btn = document.getElementById("btnRefresh");
    btn.addEventListener('click', handleRefreshClick);
}

function handleRefreshClick(event) {
    getHarvests();
}

function getHarvests() {

    fetch('/api/Harvests', {
        method: 'GET',
        headers: {
            'Accept': 'application/json'
        }
    })
        .then(res => {
            if (res.ok) {
                return res.json();
            } else {
                alert(`Server status: ${res.status}`);
            }
        })
        .then(data => {
         
            updateHarvestList(data);
        })
        .catch(err => {
            console.error(err);
            alert("Unexpected error occurred.");
        });
}

function updateHarvestList(harvests) {
    const tbody = document.getElementById("harvestTableBody");
    
    tbody.innerHTML = '';
    
    for (const harvest of harvests) {
       

        const rowHtml = `
            <tr>
                <td>${harvest.cropType}</td>
                <td>${harvest.quantity} kg</td>
                <td>${harvest.harvestDate}</td>
            </tr>
        `;
        
        tbody.insertAdjacentHTML("beforeend", rowHtml);
    }
}