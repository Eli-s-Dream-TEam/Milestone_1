#include "dictionary.h"
#include <stdlib.h> 
#include <stdio.h>
typedef struct Dictionary {
	int key;
	int value;
    struct Dictionary* left;
    struct Dictionary* right;
} Dictionary;

Dictionary* initDictionary() {
	Dictionary* d = malloc(sizeof(Dictionary));
	if (d == NULL) {
		return NULL;
	}
	d->left = NULL;
	d->right = NULL;
	return d;
}

void destroyDictionary(Dictionary* d) {
	if (d == NULL) {
		return;
	}
	destroyDictionary(d->left);
	destroyDictionary(d->right);
	free(d);
}

int sizeOfDictionary(Dictionary* d) {
	if (d == NULL) {
		return 0;
	}
	return 1 + sizeOfDictionary(d->left) + sizeOfDictionary(d->right);
}

Result putInDictionary(Dictionary* d, int key, int value) {
	if (d == NULL) {
		return FAILURE;
	}
	if (d->key > key) {
		if (d->left != NULL) {
			return putInDictionary(d->left, key);
		}
		d->left = initDictionary();
		if (d->left == NULL) {
			return MEM_ERROR;
		}
		else {
			return SUCCESS;
		}
	}
	if (d->val < key) {
		if (d->right != NULL) {
			return add_to_bin_tree(d->right, key);
		}
		d->right = initDictionary();
		if (d->right == NULL) {
			return MEM_ERROR;
		}
		else {
			return SUCCESS;
		}
	}
	d->value = value;
	return SUCCESS;
}

int getFromDictionary(Dictionary* d, int key) {
	if (d == NULL) {
		return NULL;
	}
	if (d->key > key) {
		return getFromDictionary(d->left, key);
	}
	if (d->key < key) {
		return getFromDictionary(d->right, key);
	}
	return d->value;
}

Result removeFromDictionary(Dictionary* d, int key) {

}