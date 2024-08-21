//*****************************************************************************
//** 217. Contains Duplicate  leetcode                                       **
//** Two solutions, both work.  Hash Table above (faster) and loop below     **
//**                                                               -Dan      **
//*****************************************************************************


// Hash table node
typedef struct HashNode {
    int key;
    struct HashNode* next;
} HashNode;

// Hash table structure
typedef struct {
    HashNode** table;
    int size;
} HashTable;

// Hash function
unsigned int hash(int key, int size) {
    return abs(key) % size;
}

// Create a new hash node
HashNode* createNode(int key) {
    HashNode* newNode = (HashNode*)malloc(sizeof(HashNode));
    newNode->key = key;
    newNode->next = NULL;
    return newNode;
}

// Insert key into hash table
bool insert(HashTable* hashTable, int key) {
    unsigned int hashIndex = hash(key, hashTable->size);
    HashNode* newNode = createNode(key);
    
    // Check if key already exists
    HashNode* current = hashTable->table[hashIndex];
    while (current != NULL) {
        if (current->key == key) {
            free(newNode); // Free the new node since the key already exists
            return false;
        }
        current = current->next;
    }

    // Insert new node at the beginning of the linked list at table[hashIndex]
    newNode->next = hashTable->table[hashIndex];
    hashTable->table[hashIndex] = newNode;
    return true;
}

// Create a new hash table
HashTable* createHashTable(int size) {
    HashTable* hashTable = (HashTable*)malloc(sizeof(HashTable));
    hashTable->size = size;
    hashTable->table = (HashNode**)calloc(size, sizeof(HashNode*));
    return hashTable;
}

// Free hash table memory
void freeHashTable(HashTable* hashTable) {
    for (int i = 0; i < hashTable->size; i++) {
        HashNode* current = hashTable->table[i];
        while (current != NULL) {
            HashNode* temp = current;
            current = current->next;
            free(temp);
        }
    }
    free(hashTable->table);
    free(hashTable);
}

bool containsDuplicate(int* nums, int numsSize) {
    HashTable* hashTable = createHashTable(numsSize);

    for (int i = 0; i < numsSize; i++) {
        if (!insert(hashTable, nums[i])) {
            freeHashTable(hashTable); // Clean up the hash table before returning
            return true;  // Duplicate found
        }
    }

    freeHashTable(hashTable); // Clean up the hash table after processing
    return false;  // No duplicates found
}

/*
// Comparison function for qsort
int compare(const void *a, const void *b) {
    return (*(int*)a - *(int*)b);
}

bool containsDuplicate(int* nums, int numsSize) {

    qsort(nums, numsSize, sizeof(int), compare);

    for(int i = 1; i < numsSize; i++)
    {
        if(nums[i - 1] == nums[i]) return true;
    }
    return false;
}
*/