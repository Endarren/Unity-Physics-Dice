Today, we will be making a script that can determine the value of a die based on which side is facing up, similar to how they work in Table Top Simulator.  I will be using a dice model asset pack from the asset store.  The link to it is in the description.  It cost a $1, though I got it when it was free. The models are pretty good and there are high, medium, and low quality versions of them.  They also include a dice physics material.  

So let's get to work.

We will start off by create a script called GameDie.

In this script, we will add an int variable called dieValue.  This will be used to store the current value of the die.

Next, we are going to create a struct in the same file.  We'll call this struct FacingData.  We will give it two variables; an int for a face value and a Vector3 for the direction.  The direction will be the upward direction of the die when this face value is on top.  We will use this to determine the value of the die.  We are also going to put above the struct [Serializable].  This will make the struct appear in the inspector.

Now, we make a list of FacingData in the GameDie class, and call it facing.

After that, we go to make a method called DetermineFaceValue.  In this method, we will need three variables; a float called max, which we will set to -1, and an int called match, which we will also set to 0, and finally a float called dotValue.  We don't need to give dotValue a value right now.

Next, we make a loop to go through all of the facing list.  In this loop, we will set dotValue to equal the dot product of the transform.rotation times the current facing and Vector3 up.  The goal is to find the dot product with the highest value.  Dot products can be values from -1 to 1.  The higher the value, the closer the two vector3s are to each other.  This means that the facing direction that makes the highest dot product is the one closest to the up direction.

So once we have a dot product, we compare it against max.  if it is higher than max, replace max with dotValue and set match to the faceValue of this facing.  

Once the loop finishes, we set dieValue to match.

Now to test this out, we have a call to DetermineFaceValue in Update and go back to Unity.  Next, we set the facings for the dice.  For this test, we will use a D6 or a six sided die, because it is the simplest to set up.

Once all the faces are set, we hit play and test it out.  We rotate the die to see if the correct value is set based on the up side.  

And as you can see, it works.