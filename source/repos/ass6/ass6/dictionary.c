/****************
* Aviv Ben Yaakov
* 206261695
* 01
* ass06
*****************/
#include "dictionary.h"
#include <stdlib.h> 
#include <stdio.h>


struct Node {
	int value;
	int key;
	struct Node* next;
};

struct Dictionary {
	struct Node* head;
	struct Node* current;
};


/******************
* Function Name: initDictionary
* Input: none
* Output: initialized dictionary.
* Function Operation: this function create dictionary from struct and return it
******************/

Dictionary* initDictionary() {

	// allocate memory to Dictoinary struct
	Dictionary* d = (Dictionary*)malloc(sizeof(Dictionary));
	if (d == NULL) {
		return NULL;
	}
	// initialized fields withe NULL to represent empty dictionary.
	d->head = NULL; 
	d->current = NULL;

	// return initialized dictionary.
	return d;
}


/******************
* Function Name: destroyDictionary
* Input: dictionary.
* Output: none.
* Function Operation: this free all memory allocated to the dictionary.
******************/
void destroyDictionary(Dictionary* d) {

	// creating temporary node.
	struct Node* temp;
	if (d == NULL) {
		return;
	}
	// iterate over the dictionry and free all memory
	if (d->head != NULL) {
		while (d->head->next != NULL) {
			temp = d->head->next;
			free(d->head);
			d->head = temp;
		}
	}

	free(d->head);
	free(d->current);
	free(d);
}

/******************
* Function Name: sizeOfDictionary.
* Input: dictionary.
* Output: size of dictionary.
* Function Operation: this function receives dictionary, and returns it's size to the user.
******************/
int sizeOfDictionary(Dictionary* d) {
	int size = 0;
	// iterate over the dictionary and count the number of items inside.
	for (d->current = d->head; d->current != NULL; d->current = d->current->next) {
		size++;
	}
	return size;
}


/******************
* Function Name: putInDictionary.
* Input: dictionary,key and value.
* Output: Result(MEM_ERROR/SUCCESS)
* Function Operation: this function receives dictionary, key and value and insert key-value pair
into dictionary. if insertion succeded return suitable message.
******************/
Result putInDictionary(Dictionary* d, int key, int value) {
	// create a node
	struct Node* node = (struct Node*) malloc(sizeof(struct Node));
	
	// if memory allocation failed free memory and return error
	if (node == NULL){
		destroyDictionary(d);
		return MEM_ERROR;
	}
	if (d->head != NULL)
		// if key already exist in the dictionary update it's value
		removeFromDictionary(d, key); 
	node->key = key; 
	node->value = value;
	//point it to old first node
	node->next = d->head;

	//point first to new first node
	d->head = node;

	// if insertion succeeded
	return SUCCESS;

}


/******************
* Function Name: getFromDictionary.
* Input: dictionary,key.
* Output: value or 0.
* Function Operation: this function receives dictionary,and key and returns the value of 
the given key. if key not exist in the dictionary, then return 0.
******************/
int getFromDictionary(Dictionary* d, int key) {

	//if dictionary is empty
	if (d->head == NULL) {
		return 0;
	}
	//start from the first link
	struct Node* current = d->head;

	while (current->key != key) {
		//if it is last node
		if (current->next == NULL) {
			return 0;
		}
		else {
			//go to next node
			current = current->next;
		}
	}
	//if key found, return the current value
	return current->value;
}


/******************
* Function Name: removeFromDictionary.
* Input: dictionary,key.
* Output: Result(SUCCESS/FAILURE).
* Function Operation: this function receives dictionary,and key. using the key, the function
removes the key-value pair from the dictionary. if removal succeded return SUCCESS, otherwise - return FAILURE.
******************/
Result removeFromDictionary(Dictionary* d, int key) {
	//start from the first node
	struct Node* current = d->head;
	struct Node* previous = NULL;
	//if dictionary is empty
	if (d->head == NULL) {
		return FAILURE;
	}
	//navigate through the dictionary
	while (current->key != key) {

		//if it is last node
		if (current->next == NULL) {
			return FAILURE;
		}
		else {
			//store reference to current node
			previous = current;
			//move to next node
			current = current->next;
		}
	}
	//found a match, update the node
	if (current == d->head) {
		//change first to point to next node
		d->head = d->head->next;
	}
	else {
		//bypass the current node
		previous->next = current->next;
	}

	free(current);
	// if item removal succeeded
	return SUCCESS;
}

/******************
* Function Name: createDictionaryFromArrays.
* Input: array of keys and array of values and thier size.
* Output: updated dictionary.
* Function Operation: this function recieves two arrays, one of keys and the other of values.
the function iterate over the arrays and with the help of putInDictionary function insert key-value pair into
one by one.
******************/
Dictionary* createDictionaryFromArrays(int keys[], int values[], int size) {
	Dictionary* d = initDictionary();
	for (int i = 0; i < size; i++) {

		// insert new key-value pair inside the dictionary.
		putInDictionary(d, keys[i], values[i]);
	}
	
	// return updated dictionary.
	return d;
}

/******************
* Function Name: sortDictionary.
* Input: dictionary.
* Output: none.
* Function Operation: this function recieves dictionary and organize it by key value(from smallest to largest).
******************/
void sortDictionary(Dictionary* d) {

	int k, tempKey, tempValue;
	struct Node* current;
	struct Node* next;

	// initialize size with the size of dictionary.
	int size = sizeOfDictionary(d);
	k = size;

	for (int i = 0; i < size - 1; i++, k--) {
		current = d->head;
		next = d->head->next;

		for (int j = 1; j < k; j++) {

			// if current key bigger then next one, swap them.
			if (current->key > next->key) {
				tempValue = current->value;
				current->value = next->value;
				next->value = tempValue;

				tempKey = current->key;
				current->key = next->key;
				next->key = tempKey;
			}

			// move to next item
			current = current->next;
			next = next->next;
		}
	}
}


/******************
* Function Name: printDictionary.
* Input: dictionary.
* Output: none.
* Function Operation: this function recieves dictionary, and print it's content sorted by 
key value(from smallest to largest).
******************/
void printDictionary(Dictionary* d) {

	// calling sort function to organize the dictionary.
	sortDictionary(d);
	struct Node* ptr = d->head;
	printf("{");

	// iterate through the dictionary and print it's content
	while (ptr != NULL) {
		printf("[%d:%d]", ptr->key, ptr->value);
		ptr = ptr->next;
	}

	printf("}");
	free(ptr);
}


