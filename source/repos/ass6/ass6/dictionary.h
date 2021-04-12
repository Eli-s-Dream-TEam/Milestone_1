#ifndef __DICTIONARY_H
#define __DICTIONARY_H

#include "result.h"
typedef struct Dictionary Dictionary;

Dictionary* initDictionary();
void destroyDictionary(Dictionary* d);

int sizeOfDictionary(Dictionary* d);
Result putInDictionary(Dictionary* d, int key, int value);
int getFromDictionary(Dictionary* d, int key);
Result removeFromDictionary(Dictionary* d, int key);

void printDictionary(Dictionary* d);

Dictionary* createDictionaryFromArrays(int keys[], int values[], int size);



#endif

