async function showEquipment() {
    try {
        const response = await fetch('/account/show-equipment', {
            method: 'GET',
        });
        if (response.ok) {
            const data = await response.json();
            displayItems(data.equipmentDatas[0].items);
        } else {
            console.error('Error fetching and showing equipment info:', response.statusText);
        }
    } catch (error) {
        console.error('Error:', error);
    }
}

async function login() {
    try {
        window.location.href = '/account/login';
    } catch (error) {
        console.error('Error during login:', error);
    }
}

async function getItemIconUrl(itemHash) {
    try {
        const response = await fetch(`/database-item-data/get-item-icon?itemHash=${itemHash}`);
        if (response.ok) {
            return await response.text();
        } else {
            console.error('Error fetching item icon URL:', response.statusText);
            return '';
        }
    } catch (error) {
        console.error('Error:', error);
        return '';
    }
}

async function displayItems(items) {
    const itemContainer = document.getElementById('item-container');
    itemContainer.innerHTML = '';

    for (const item of items) {
        const iconUrl = await getItemIconUrl(item.itemHash);
        const itemDiv = document.createElement('div');
        itemDiv.classList.add('item');
        itemDiv.innerHTML = `
            <img src="${iconUrl}" alt="Item Icon">
            <div class="power-level">${item.powerLevel}</div>
        `;
        itemContainer.appendChild(itemDiv);
    }
}
