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
            const equippedItemsContainer = document.getElementById(`equipped-items-${(i * 3) + j}`);
            equippedItemsContainer.innerHTML = '';
            const itemElement = document.createElement('div');
            itemElement.classList.add('item');
            itemElement.style.backgroundImage = `url(${await getItemIcon(characterDatas[j].items[i].itemHash)})`;
            itemElement.onclick = () => showPopup(characterDatas[j].items[i].itemInstanceId);
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
                const additionalItemElement = document.createElement('div');
                additionalItemElement.classList.add('item');
                additionalItemElement.style.backgroundImage = `url(${await getItemIcon(currentSlotItems[k].itemHash)})`;
                additionalItemElement.onclick = () => showPopup(currentSlotItems[k].itemInstanceId);
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

        const itemElement = document.createElement('div');
        itemElement.classList.add('item');
        itemElement.style.backgroundImage = `url(${await getItemIcon(data.kinetic[i].itemHash)})`;
        itemElement.onclick = () => showPopup(data.kinetic[i].itemInstanceId);
        kineticVaultContainer.appendChild(itemElement);
    }

    const energyVaultContainer = document.getElementById(`items-grid-energy-vault`);
    energyVaultContainer.innerHTML = '';
    
    for (let i = 0; i < data.energy.length; i++){        

        const itemElement = document.createElement('div');
        itemElement.classList.add('item');
        itemElement.style.backgroundImage = `url(${await getItemIcon(data.energy[i].itemHash)})`;
        itemElement.onclick = () => showPopup(data.energy[i].itemInstanceId);
        energyVaultContainer.appendChild(itemElement);
    }
    const heavyVaultContainer = document.getElementById(`items-grid-heavy-vault`);
    heavyVaultContainer.innerHTML = '';
    
    for (let i = 0; i < data.heavy.length; i++){        

        const itemElement = document.createElement('div');
        itemElement.classList.add('item');
        itemElement.style.backgroundImage = `url(${await getItemIcon(data.heavy[i].itemHash)})`;
        itemElement.onclick = () => showPopup(data.heavy[i].itemInstanceId);
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

function showPopup(itemInstanceId) {
    const popup = document.getElementById("popup");
    const itemInfo = document.getElementById("popup-item-info");

    getItemDetails(itemInstanceId).then(details => {
        itemInfo.innerHTML = details;
        popup.style.display = "flex";
    });
}

function closePopup() {
    const popup = document.getElementById("popup");
    popup.style.display = "none";
}

async function getItemDetails(itemInstanceId) {
    try {
        const response = await fetch(`/character-item-data/get-item-instance?itemInstanceHash=${itemInstanceId}`);
        if (response.ok) {
            return await response.text();
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
