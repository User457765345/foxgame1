using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playercontrol : MonoBehaviour

{
	public Rigidbody2D rb;
	public Collider2D coll;

	public float speed=450f;
	public float jumpforce=400f;
    public Animator anim;
	public LayerMask ground;
	public int cherry=0;

	public Text cherrynum;
	private bool ishurt;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
		if(!ishurt)
        {movement();}
		switchanim();
    }
	void movement()
		{
		float horizontalmove;
		horizontalmove=Input.GetAxis("Horizontal");

		float facedirection=Input.GetAxisRaw("Horizontal");

		if(horizontalmove !=0)
			{
			rb.velocity=new Vector2(horizontalmove*speed*Time.deltaTime,rb.velocity.y);
			anim.SetFloat("running",Mathf.Abs(facedirection));
			}
		if (facedirection !=0)
		{
			transform.localScale=new Vector3(facedirection,1,1);
		}

		if (Input.GetButtonDown("Jump")&&coll.IsTouchingLayers(ground))
		{
			rb.velocity=new Vector2(rb.velocity.x,jumpforce*Time.deltaTime);
			anim.SetBool("jumping",true);
		}



		}




	//切换动画效果
        void switchanim()
	    {
			anim.SetBool("idle",false);
		if (anim.GetBool("jumping"))
		{
			if (rb.velocity.y<0)
			{
				anim.SetBool("jumping",false);
				anim.SetBool("falling",true);
			}
		}
		else if(ishurt)
			{anim.SetBool("hurt",true);
		     anim.SetFloat("running",0);
			if (Mathf.Abs(rb.velocity.x)<0.1f)
				{anim.SetBool("hurt",false);
			     anim.SetBool("idle",true);
				ishurt=false;}
			
		}
		else if(coll.IsTouchingLayers(ground))
			{
			anim.SetBool("falling",false);
			anim.SetBool("idle",true);
		}
		}

//收集
		private void OnTriggerEnter2D (Collider2D collision)
	    {
			if(collision.tag=="collection")
			{Destroy(collision.gameObject);cherry+=1;
			 cherrynum.text= cherry.ToString();}
			
		}

//消灭敌人
         private void OnCollisionEnter2D (Collision2D collision)
	{
			 if (collision.gameObject.tag=="enemy")
			 {
			 
			   if(anim.GetBool("falling"))
			  {Destroy(collision.gameObject);
			   rb.velocity=new Vector2(rb.velocity.x,jumpforce*Time.deltaTime);
			anim.SetBool("jumping",true);
			   }
			    else if(transform.position.x<collision.gameObject.transform.position.x)
				 {rb.velocity= new Vector2(-5,rb.velocity.y);
				ishurt=true;}
			     else if(transform.position.x>collision.gameObject.transform.position.x)
				 {rb.velocity= new Vector2(5,rb.velocity.y);ishurt=true;}
			 }
		 
		 }

}
