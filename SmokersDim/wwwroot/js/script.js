async function showEquipment() {
    try {
        const response = await fetch('/account/show-equipment', { method: 'GET' });
        if (response.ok) {
            const data = await response.json();
            displayCharactersEquipment(data.characterDatas);
        } else {
            console.error('Error fetching and showing equipment info:', response.statusText);
        }
    } catch (error) {
        console.error('Error:', error);
    }
}

async function displayCharactersEquipment(characterDatas) {
    // change to all equipment slots
    for (let i = 0; i < characterDatas.length; i++) 
    {
        let startingBucketIndex = 0;
        for (let j = 0; j < 3; j++)
        {
            const equippedItemsContainer = document.getElementById(`equipped-items-${(i * 3) + j}`);
            equippedItemsContainer.innerHTML = '';
            const itemElement = document.createElement('div');
            itemElement.classList.add('item');
            itemElement.style.backgroundImage = `url(${await getItemIcon(characterDatas[i].items[j].itemHash)})`;
            equippedItemsContainer.appendChild(itemElement);
            
            const additionalItemsContainer = document.getElementById(`additional-items-${(3 * i) + j}`);
            additionalItemsContainer.innerHTML = '';

            let currentBucketHash = 0;
            if (j == 0)
                currentBucketHash = 1498876634;
            else if (j == 1)
                currentBucketHash = 2465295065;
            else if (j == 2)
                currentBucketHash = 953998645;
            
            for (let k = startingBucketIndex; k < startingBucketIndex + 10; k++)
            {
                let currentItem = characterDatas[i].inventoryItems[k];
                if (currentItem.bucketHash != currentBucketHash){
                    startingBucketIndex = k;
                    break;
                }
                const itemElement = document.createElement('div');
                itemElement.classList.add('item');
                itemElement.style.backgroundImage = `url(${await getItemIcon(currentItem.itemHash)})`;
                additionalItemsContainer.appendChild(itemElement);
            }
        }
    }
}

async function getItemIcon(itemHash) {
    try {
        const response = await fetch(`/database-item-data/get-item-icon?itemHash=${itemHash}`);
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

async function login() {
    try {
        window.location.href = '/account/login';
    } catch (error) {
        console.error('Error during login:', error);
    }
}
