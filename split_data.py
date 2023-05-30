import glob
import os
from random import shuffle
import shutil
from sklearn.model_selection import train_test_split
import numpy as np
 

#7 ways to loop through a list https://learnpython.com/blog/python-list-loop/
  
# Defining main function
def main(ori_data_path,train_data_folder,test_data_folder):
    fileList=glob.glob(ori_data_path)
    X_train,X_test=train_test_split(fileList,train_size=0.7,random_state=0,shuffle=True)
    os.makedirs(train_data_folder, exist_ok=True)
    os.makedirs(test_data_folder, exist_ok=True)
    #Convert a list to numpy array to make iteration faster when the list is big
    #Because NumPy reduces the overhead by making iteration more efficient. 
    numpyX_train = np.array(X_train)
    numpyX_test = np.array(X_test)
    for file in numpyX_train:
        shutil.copy(file,train_data_folder)
    for file in numpyX_test:
        shutil.copy(file,test_data_folder)
  
  
# Using the special variable 
# __name__
if __name__=="__main__":
    ori_data_path=r'C:\Users\Ben Chen\Documents\Ben_AI_Tryouts\Keras_MultiLabel_Classification\Movie_Poster_Dataset\2014\*.jpg'
    train_data_folder=r'.\train'
    test_data_folder=r'.\test'
    main(ori_data_path,train_data_folder,test_data_folder)



