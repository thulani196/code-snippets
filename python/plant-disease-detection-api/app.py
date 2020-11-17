import io
import re
import json
from io import BytesIO

from torchvision import models
import torch
from torch import nn
from PIL import Image
from torch.autograd import Variable
from collections import OrderedDict
import torchvision.transforms as transforms
import numpy as np
import numpy
import base64
from flask import Flask, jsonify, request
app = Flask(__name__)

def transform_image(image_bytes):
    # image = base64.b64decode(image_bytes, ' /')
    my_transforms = transforms.Compose([transforms.Resize(224),
                                        transforms.CenterCrop(224),
                                        transforms.ToTensor(),
                                        transforms.Normalize(
                                            [0.485, 0.456, 0.406],
                                            [0.229, 0.224, 0.225])])

    image = Image.open(io.BytesIO(image_bytes))
    return my_transforms(image).unsqueeze(0)

def get_prediction(image_bytes):
    topk = 3
    tensor = transform_image(image_bytes = image_bytes)

    results = { "predictions": [] }
    result = None

    model.eval()
    outputs = model.forward(Variable(tensor))
    probabilities = torch.exp(outputs).data.numpy()[0]
    
    top_idx = np.argsort(probabilities)[-topk:][::-1] 
    top_class = [idx_to_class[x] for x in top_idx]

    top_probability = probabilities[top_idx]
    
    for i in range(len(top_class)):
        result = {}
        result['class_label'] = re.sub(r'\s+', ' ', str(top_class[i]).replace('_', ' ')) # Replace underscores with spaces, then replace extra spaces with single space using -re
        result['accuracy'] = top_probability[i] * 100 
        results['predictions'].append(result)

    results = json.dumps(results)
    return results

def load_model(path):
    checkpoint = torch.load(path)
    clf = models.alexnet(pretrained=True)
    output = 8

    classifier = nn.Sequential(OrderedDict([
        ('fc1', nn.Linear(9216, 4096)),
        ('relu', nn.ReLU()),
        ('dropout_2', nn.Dropout(0.5)),
        ('fc2', nn.Linear(4096, 8)),
        ('output', nn.LogSoftmax(dim=1))
    ]))

    clf.classifier = classifier
    clf.load_state_dict(checkpoint['state_dict'])

    return clf, checkpoint['class_to_idx']

# Load Model
model, index_classname  = load_model('model/alexnet_output.pth')
idx_to_class = { v : k for k,v in index_classname.items()}

@app.route('/predict', methods=['POST'])
def predict():
    # image = request.args['image']
    if request.method == 'POST':
        if request.files:
            file = request.files['image']
            img_bytes = file.read()
            results = get_prediction(image_bytes=img_bytes)
            return results
        else:
            return json.dumps({"error": "No attachment found."})


if __name__ == '__main__':
    app.run()