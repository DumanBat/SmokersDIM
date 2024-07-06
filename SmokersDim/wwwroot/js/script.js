async function showEquipment() {
    try {
        const response = await fetch('/account/show-equipment', { method: 'GET' });
        if (response.ok) {
            const data = await response.json();
            displayCharactersEquipment(data.characterDatas);
        } else {
            console.error('Error fetching and showing equipment info:', response.statusText);
        }        
        
        const vaultResponse = await fetch('character-item-data/get-vault-items', { method: 'GET' });
        if (vaultResponse.ok) {
            const data = await vaultResponse.json();
            displayProfileVaultData(data);
        } else {
            console.error('Error fetching and showing vault info:', vaultResponse.statusText);
        }

    } catch (error) {
        console.error('Error:', error);
    }
}

async function displayCharactersEquipment(characterDatas) {

    // 3 slots
    for (let i = 0; i < 3; i++){        
        let currentBucketHash = 0;
        if (i == 0)
            currentBucketHash = 1498876634;
        else if (i == 1)
            currentBucketHash = 2465295065;
        else if (i == 2)
            currentBucketHash = 953998645;

        for (let j = 0; j <characterDatas.length; j++){
            let item = characterDatas[j].items[i];
            const equippedItemsContainer = document.getElementById(`equipped-items-${(i * 3) + j}`);
            equippedItemsContainer.innerHTML = '';
            const itemElement = document.createElement('div');
            itemElement.classList.add('item');
            itemElement.style.backgroundImage = `url(${await getItemIcon(item.itemHash)})`;
            itemElement.onclick = () => showPopup(item.itemHash, item.itemInstanceId);
            equippedItemsContainer.appendChild(itemElement);

            const additionalItemsContainer = document.getElementById(`additional-items-${(i * 3) + j}`);
            additionalItemsContainer.innerHTML = '';

            let currentSlotItems;
            if (i == 0)
                currentSlotItems = characterDatas[j].kinetic;
            else if (i == 1)
                currentSlotItems = characterDatas[j].energy;
            else if (i == 2)
                currentSlotItems = characterDatas[j].heavy;

            for (let k = 0; k < currentSlotItems.length; k++){
                let item = currentSlotItems[k];
                const additionalItemElement = document.createElement('div');
                additionalItemElement.classList.add('item');
                additionalItemElement.style.backgroundImage = `url(${await getItemIcon(item.itemHash)})`;
                additionalItemElement.onclick = () => showPopup(item.itemHash, item.itemInstanceId);
                additionalItemsContainer.appendChild(additionalItemElement);
            }
        }
    }
}

async function displayProfileVaultData(data)
{
    const kineticVaultContainer = document.getElementById(`items-grid-kinetic-vault`);
    kineticVaultContainer.innerHTML = '';
    
    for (let i = 0; i < data.kinetic.length; i++){
        let item = data.kinetic[i];
        const itemElement = document.createElement('div');
        itemElement.classList.add('item');
        itemElement.style.backgroundImage = `url(${await getItemIcon(item.itemHash)})`;
        itemElement.onclick = () => showPopup(item.itemHash, item.itemInstanceId);
        kineticVaultContainer.appendChild(itemElement);
    }

    const energyVaultContainer = document.getElementById(`items-grid-energy-vault`);
    energyVaultContainer.innerHTML = '';
    
    for (let i = 0; i < data.energy.length; i++){     
        let item = data.energy[i];
        const itemElement = document.createElement('div');
        itemElement.classList.add('item');
        itemElement.style.backgroundImage = `url(${await getItemIcon(item.itemHash)})`;
        itemElement.onclick = () => showPopup(item.itemHash, item.itemInstanceId);
        energyVaultContainer.appendChild(itemElement);
    }
    const heavyVaultContainer = document.getElementById(`items-grid-heavy-vault`);
    heavyVaultContainer.innerHTML = '';
    
    for (let i = 0; i < data.heavy.length; i++){
        let item = data.heavy[i];
        const itemElement = document.createElement('div');
        itemElement.classList.add('item');
        itemElement.style.backgroundImage = `url(${await getItemIcon(item.itemHash)})`;
        itemElement.onclick = () => showPopup(item.itemHash, item.itemInstanceId);
        heavyVaultContainer.appendChild(itemElement);
    }
}

async function getItemIcon(itemInstanceId) {
    try {
        const response = await fetch(`/database-item-data/get-item-icon?itemHash=${itemInstanceId}`);
        if (response.ok) {
            return await response.text();
        } else {
            console.error('Error fetching item icon:', response.statusText);
            return '';
        }
    } catch (error) {
        console.error('Error:', error);
        return '';
    }
}

function showPopup(itemHash, itemInstanceId) {
    const popup = document.getElementById("popup");

    getItemDetails(itemHash, itemInstanceId).then(details => {
        popup.style.display = "flex";        
        showEquipmentDetails(details);
    });
}

function showEquipmentDetails(details) {
    let displayProperties = details.displayProperties;
    let commonData = details.item.data;
    let instanceData = details.instance.data;
    let statsData = details.stats.data.stats;
    let perksData = details.perks.data.perks;
    
    let popupContent = `
        <h2>${displayProperties.name}</h2>
        <p>${details.item.data.type}</p>
        <p>RPM: ${statsData[4284893193].value}</p>
        <div class="stats">
            ${createStatBar("Impact", statsData[4043523819]?.value || 100)}
            ${createStatBar("Range", statsData[1240592695].value - 10)}
            ${createStatBar("Stability", statsData[155624089].value - 10)}
            ${createStatBar("Handling", statsData[943549884].value - 10)}
            ${createStatBar("Reload Speed", statsData[4188031367].value - 10)}
            ${createStatBar("Aim Assistance", statsData[1345609583].value)}
            ${createStatBar("Airborne", statsData[2714457168].value)}
            ${createStatBar("Zoom", statsData[3555269338].value)}
            ${createStatBar("Recoil Direction", statsData[2715839340].value)}
        </div>
        <p>Magazine: ${statsData[3871231066].value}</p>
    `;
    
    document.getElementById('popup-item-info').innerHTML = popupContent;
    document.getElementById('popup').style.display = 'flex';
}

function createStatBar(name, value) {
    return `
        <div>
            <span class="stat-name">${name}</span>
            <span class="stat-value">${value}</span>
            <div class="stat-bar">
                <div style="width: ${value}%; background-color: ${getBarColor(value)};"></div>
            </div>
        </div>
    `;
}

function getBarColor(value) {
    if (value > 75) return '#00ff00'; // Green for high values
    if (value > 50) return '#ffff00'; // Yellow for medium values
    return '#ff0000'; // Red for low values
}


function closePopup() {
    const popup = document.getElementById("popup");
    popup.style.display = "none";
}

async function getItemDetails(itemHash, itemInstanceId) {
    try {
        const params = new URLSearchParams({
            itemHash: itemHash,
            itemInstanceHash: itemInstanceId
        });
        const response = await fetch(`/character-item-data/get-item-instance?${params.toString()}`);

        if (response.ok) {
            const jsonResponse = await response.json();
            console.log('Item Details Response:', itemInstanceId, jsonResponse);
            return jsonResponse;
        } else {
            console.error('Error fetching item details:', response.statusText);
            return 'No details available';
        }
    } catch (error) {
        console.error('Error:', error);
        return 'No details available';
    }
}

async function login() {
    try {
        window.location.href = '/account/login';
    } catch (error) {
        console.error('Error during login:', error);
    }
}
